using System;
namespace BinaryBoarding
{
    public class SeatingCode
    {

        public string Code { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int Id { get; set; }

        public SeatingCode(string code, int row, int column, int id)
        {
            Code = code;
            Row = row;
            Column = column;
            Id = id;
        }

        public SeatingCode(string code)
        {
            Code = code;
            Row = 0;
            Column = 0;
            Id = 0;
        }

    }
}
