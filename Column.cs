namespace CodeGeneratorV1
{
    public class Column
    {
        public int Id { get; set; }
        public string DataField { get; set; }
        public bool IsPrimaryKey { get; set; }
        public string DataType { get; set; }
        public int? MaxLength { get; set; }
        public int? Position { get; set; }
        public Column(int id, string dataField, bool isPrimaryKey, string dataType, int? maxLength, int? position)
        {
            Id = id;
            DataField = dataField;
            IsPrimaryKey = isPrimaryKey;
            DataType = dataType;
            MaxLength = maxLength;
            Position = position;
        }
    }

}
