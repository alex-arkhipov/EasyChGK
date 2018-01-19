// Class to work with remote ChGK DB

using System;
using System.Diagnostics.Contracts;
using Foundation;
using System.IO;

namespace EasyChGK.Neo
{
    public static class ChgkExternal
    {
        const String QUESTIONS_URL = "https://db.chgk.info/xml/random/types1/limit";
        const String IMAGE_URL = "https://db.chgk.info/images/db/";
        const int NUM_OF_QUESTIONS = 12;

        private static String GetImageURL(String image_name)
        {
            return IMAGE_URL + image_name;
        }

        private static String GetQuestionsURL(int num = NUM_OF_QUESTIONS)
        {
            return QUESTIONS_URL + num.ToString();
        }

        // Downloads XML with questions and returns its text representation
        public static String GetQuestionsXML(int num = NUM_OF_QUESTIONS)
        {
            String s = "";
            var q_url = GetQuestionsURL(num);
            using (var url = new NSUrl(q_url))
            using (var data = NSData.FromUrl(url))
            {
               s = data.ToString();
            }
            return s;
        }

        public static String GetQuestionsXMLTest()
        {
            Contract.Ensures(Contract.Result<string>() != null);
            //Console.WriteLine(String.Join(Environment.NewLine, Directory.GetFiles(Directory.GetCurrentDirectory())));
            
            string text = System.IO.File.ReadAllText(@"test_questions.xml");
            return text;
        }

        // Get image URL
        public static String GetPictureURL(String pic_fn)
        {
            return IMAGE_URL + pic_fn;
        }
    }


}
