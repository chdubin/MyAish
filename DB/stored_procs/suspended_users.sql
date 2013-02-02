-- ApplicationServices Sql MsSql2005
 DECLARE @cur_app_name varchar(50)

 SET @cur_app_name = 'AishAudio'

SELECT
	[m].[UserId],
	[t2].[UserName],
	[m].[firstName],
	[m].[lastName],
	[t1].[Email],
	mc.membershipCartID AS [Last 4 Digits],
	CONVERT(varchar(7), [mc].[expirationDate], 126) as [Exp Date],
	mc.cartTypeID AS [Card Type],
	CAST([m].[balance] as int) as Units,
	[m].[chargeDay],
	CONVERT(varchar(20), [m].[subscribeActivation], 101) as [Plan Bought On],
	CONVERT(varchar(20), [t4].[endSubscribeDate], 101) AS [Plan Expired On],
	CONVERT(varchar(20), [t4].[activationDate], 101) AS [Last Charged On],
	CASE [m].[activatedCart] WHEN 0 THEN 'Yes' WHEN 1 THEN 'No' END AS [Card Active],
	CASE [t4].[subscribePlanID] WHEN 238 THEN 'Monthly' ELSE CAST([t4].[subscribePlanID] AS varchar(4)) END AS [Card Active],
	CASE WHEN [m].[activationCartDate] IS NULL THEN '' ELSE CONVERT(varchar(20), [m].[activationCartDate], 101) END as [CC Added On],
	[m].[postalAdderss] AS [Street],
	[m].[city],
	[m].[state] as [State],
	[m].[postalCode] as Zip,
	[m].[country],
	[m].[dayPhone],
	[m].[phone]
FROM
	[dbo].[Membership] [m]
		INNER JOIN [dbo].[aspnet_Membership] [t1] ON [m].[UserId] = [t1].[UserId]
		INNER JOIN [dbo].[aspnet_Users] [t2] ON [m].[UserId] = [t2].[UserId]
		INNER JOIN [dbo].[aspnet_Applications] [t3] ON [t2].[ApplicationId] = [t3].[ApplicationId]
		LEFT JOIN [dbo].[MembershipCart] [mc] ON [m].[UserId] = [mc].[UserId]
		LEFT JOIN [dbo].[MembershipXrefSubscribePlan] [t4] ON [m].[UserId] = [t4].[UserId]
		LEFT JOIN [dbo].[EntityItem] [t5] ON [t4].[subscribePlanID] = [t5].[entityID]
		LEFT JOIN [dbo].[MembershipXrefReferrer] [t6] ON [m].[UserId] = [t6].[UserId]
WHERE
	[t3].[ApplicationName] = @cur_app_name AND [m].[suspended] = 1 AND
	--(([m].chargeDay is null or [m].chargeDay = 0) or
	--[t4].[endSubscribeDate] < '5/12/2012') and
	[t4].[subscribePlanID] IN (237, 238, 27495)
ORDER BY
	[m].[subscribeActivation] DESC,
	[t1].[CreateDate] DESC

