namespace Scores364.Core.Datasources.Interfaces
{
    public interface IDatasourcesFabric
    {
        IDatasource Build(DatasourceInfo dsInfo);
    }
}
