SELECT a.AppName, a.MetaData, a.AppID, a.AppDesc, a.ImageURL, a.OwnerUserID, a.PillarID, a.SupportOwnerID, a.URL, b.AudienceID, b.BuinessOwnerID, b.ClassificationID, b.FormatID, b.FrequencyID, b.KeyWord, b.ProcessID
FROM COE.dbo.tblApplication a 
INNER JOIN COE.dbo.tblHelp_Application b 
ON a.AppID = b.AppID 
Where b.AppID = '15'