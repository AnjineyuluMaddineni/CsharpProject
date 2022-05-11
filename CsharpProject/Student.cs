using System.Collections.Generic;

namespace CsharpProject
{
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Degree { get; set; }
        //public List <string> Hobbies { get; set; }

        public override string ToString()
        {
            return string.Format("Student Information:\n\tId: {0}, \n\tName: {1},\n\t",
                                                                Id, Name);
        }
    }
}
