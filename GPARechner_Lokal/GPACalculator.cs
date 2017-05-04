using System.IO;
using System.Xml.Serialization;


namespace GPARechner_Lokal
{
    public class GPACalculator
    {


        public GPACalculator()
        {

        }


        public double calculateGPA(Lecture[] lectures)
        {
            double sum = 0;
            double weightSum = 0;
            foreach (Lecture lecture in lectures)
            {
                if (lecture.Note > 0)
                {
                    sum += lecture.Weight * lecture.Note;
                    weightSum += lecture.Weight;
                }
                else
                {
                    //throw new Exception("there is something weird");
                }
            }
            return sum / weightSum;
        }

        public string SerializeLectures(Lecture[] lectures)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Lecture[]));
            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, lectures);
            ms.Position = 0;
            StreamReader reader = new StreamReader(ms);
            return reader.ReadToEnd();
        }
    }

}