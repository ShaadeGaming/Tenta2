--Uppgift 8 AddTerritory procedur

CREATE PROCEDURE AddTerritory 
	-- Add the parameters for the stored procedure here
	@TerritoryID nvarchar(20),
	@TerritoryDescription nchar(50), 
	@RegionID int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO Territories
           (TerritoryID,TerritoryDescription,RegionID)
     VALUES
           (@TerritoryID,@TerritoryDescription,@RegionID)


END
GO
exec AddTerritory'någotannat','Stockholm',2