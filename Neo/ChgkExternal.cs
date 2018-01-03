// Class to work with remote ChGK DB

using System;
using Foundation;

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
    }


}
