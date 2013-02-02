using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Threading;
using System.Web.Script.Serialization;
using Amazon.S3.Model;

namespace Main.Common.AmazonS3
{
    public class S3Daemon:IDisposable
    {
        //private string _awsAccessKey;
        //private string _awsSecretKey;
        private Amazon.S3.AmazonS3 _client;
        private string _bucketName;
        private string _rootKey;

        private string _uploadTempFolder;
        private Thread _workThread;
        private Thread _updateThread;
        private string _tempUploadDir;

        protected bool StopDaemon { get; set; }
        protected AutoResetEvent DaemonStoped { get; private set; }



        private object _syncObject = new object();
        private DateTime _lastModify;
        private S3Hierarchy _hierarchy;
        public S3Hierarchy Hierarchy
        {
            get
            {
                if (DateTime.Now.Subtract(_lastModify).TotalSeconds > GlobalConstant.S3AMAZON_IN_CACHE_SEC && !_updateThread.IsAlive)
                {
                    _updateThread = new Thread(new ThreadStart(UpdateHierarchy));
                    _updateThread.Start();
                }

                return _hierarchy;
            }
        }

        public S3Daemon(string aws_access_key, string aws_secret_key, string bucket_name, string root_key, string upload_temp_folder)
        {

            //_awsAccessKey = aws_access_key;
            //_awsSecretKey = aws_secret_key;
            StopDaemon = false;
            DaemonStoped = new AutoResetEvent(false);
            _bucketName = bucket_name;
            _rootKey = root_key;
            _uploadTempFolder = upload_temp_folder;
            _client = Amazon.AWSClientFactory.CreateAmazonS3Client(aws_access_key, aws_secret_key);
            _tempUploadDir = HttpContext.Current.Server.MapPath(Properties.Settings.Default.UploadTemporaryPath);


            _updateThread = new Thread(new ThreadStart(UpdateHierarchy));
            _updateThread.Start();

            _workThread = new Thread(new ThreadStart(Do));
            _workThread.Start();

        }

        public string GetPreSignedUrlRequest(string key)
        {
            var request = new Amazon.S3.Model.GetPreSignedUrlRequest()
                .WithBucketName(_bucketName)
                .WithKey(_rootKey + key)
                .WithExpires(DateTime.Now.AddDays(1))
                .WithProtocol(Protocol.HTTPS);

            return _client.GetPreSignedURL(request);
        }

        private void UpdateHierarchy()
        {
            var hierarchy = GetAllFiles();
            _hierarchy = hierarchy;
            _lastModify = DateTime.Now;
        }


        private S3Hierarchy GetAllFiles()
        {
            S3Hierarchy rval = null;

			try
			{
				var request = new Amazon.S3.Model.ListObjectsRequest().WithBucketName(_bucketName).WithPrefix(_rootKey);
				var objects = new List<S3Object>();
				var isTruncated = false;
				do
					using (var response = _client.ListObjects(request))
					{
						objects.AddRange(response.S3Objects);
						isTruncated = response.IsTruncated;
						request = request.WithMarker(response.NextMarker);
					}
				while (isTruncated);
				var tempFiles = System.IO.Directory.GetFiles(_tempUploadDir, "*.meta").Select(f =>
				{
					var se = new JavaScriptSerializer();
					return se.Deserialize<S3File>(System.IO.File.ReadAllText(f));
				});

				var files = objects.Select(o => new { path = o.Key.Substring(_rootKey.Length), size = o.Size }).
					Select(k => new S3File(k.path.Substring(0, k.path.Length - Path.GetFileName(k.path).Length), System.IO.Path.GetFileName(k.path), k.size)).
					Union(tempFiles).ToArray();
				var groups = files.OrderBy(f => f.Path).GroupBy(f => f.Path).ToArray();

				rval = new S3Hierarchy(files.Where(f => f.Size > 0).ToArray(), groups.Select(g => g.Key).ToArray());
			}
			catch
			{

			}

            return rval;
        }

        private void Do()
        {
            try
            {
                while (!StopDaemon)
                {
                    foreach (var file in Directory.GetFiles(_tempUploadDir, "*.meta"))
                    try
                    {
                        var filePath = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + ".mp3");
                        using (var data = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                        {
                            var se = new JavaScriptSerializer();
                            var meta = se.Deserialize<S3File>(System.IO.File.ReadAllText(file));
                            var request = new PutObjectRequest();
                            request.WithBucketName(_bucketName).
                                WithKey(_rootKey + meta.Path + meta.Name).
                                WithMetaData("Duration", meta.Duration.ToString()).
                                WithInputStream(data);
                            using (_client.PutObject(request)) { }
                        }
                        File.Delete(file);
                        File.Delete(filePath);
                    }
                    catch
                    {
                    }

                    Thread.Sleep(1000);
                }
            }
            finally
            {
                DaemonStoped.Set();
            }
        }

        public void Dispose()
        {
            if (_client != null)
            {
                StopDaemon = true;
                DaemonStoped.WaitOne();

                _client.Dispose();
                _client = null;
            }
        }
    }


}