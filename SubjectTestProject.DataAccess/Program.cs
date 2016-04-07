using SubjectTestProject.DataAccess.Model;
using SubjectTestProject.DataAccess.TgaTrainingComp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SubjectTestProject.DataAccess
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CourseTrainingComponentDTO course = GetCourse("ICT50715");
            foreach (var u in course.Units)
            {
                Console.WriteLine(u.Name);
                Console.WriteLine(u.AssessmentRequirements);
                Console.WriteLine(u.Elements);
            }
            Console.ReadKey();

        }
        public static CourseTrainingComponentDTO GetCourse(string code)
        {
            CourseTrainingComponentDTO course = new CourseTrainingComponentDTO();
            TrainingComponentServiceClient proxy = new TrainingComponentServiceClient("TrainingComponentServiceBasicHttpEndpoint");
            if (proxy.ClientCredentials != null)
            {
                proxy.ClientCredentials.UserName.UserName = "WebService.Read";
                proxy.ClientCredentials.UserName.Password = "Asdf098";
            }
            var serverTime = proxy.GetServerTime();
            TrainingComponentInformationRequested trainingComponentInformationRequested = new TrainingComponentInformationRequested
            {
                ShowUnitGrid = true,
                ShowReleases = true,
            };
            TrainingComponentInformationRequested trainingComponentInformationRequestedTwo = new TrainingComponentInformationRequested
            {
                ShowUnitGrid = true,
                ShowFiles = true,
                ShowReleases = true,
            };
            TrainingComponentDetailsRequest request = new TrainingComponentDetailsRequest()
            {
                Code = code,
                InformationRequest = trainingComponentInformationRequested,
            };

            //getting details for qualification
            var requestResult = proxy.GetDetails(request) as TrainingComponent;
            course.Code = requestResult.Code;
            course.Name = requestResult.Title;
            course.ParentCode = requestResult.ParentCode;
            course.ParentTitle = requestResult.ParentTitle;

            //create request objects for unit codes
            TrainingComponentDetailsRequest rr = new TrainingComponentDetailsRequest()
            {
                InformationRequest = trainingComponentInformationRequestedTwo
            };
            TrainingComponent rrs = new TrainingComponent();

            //fixes the '\' in links to '/'
            string fixSlash;

            for (int i = 0; i < requestResult.Releases[0].UnitGrid.Count(); i++)
            {
                //gets unit code from main request(qualification code) array
                rr.Code = requestResult.Releases[0].UnitGrid[i].Code;

                //gets details of unit
                rrs = proxy.GetDetails(rr);

                //create unitDTO object
                UnitTrainingComponentDTO unit = new UnitTrainingComponentDTO();
                unit.Code = rrs.Code;
                unit.Name = rrs.Title;
                unit.IsEssential = requestResult.Releases[0].UnitGrid[i].IsEssential;

                //checks for files with xml extension
                for (int ii = 0; ii < rrs.Releases[0].Files.Count(); ii++)
                {
                    //if it does, it adds it to the unitDTO object
                    if (rrs.Releases[0].Files[ii].RelativePath.Contains("xml"))
                    {

                        //replaces back slash with front slash
                        fixSlash = rrs.Releases[0].Files[ii].RelativePath.Replace("\\", "/");
                        fixSlash = "http://training.gov.au/TrainingComponentFiles/" + fixSlash;

                        if (fixSlash.Contains("Assessment"))
                        {
                            unit.AssessmentRequirements = fixSlash;
                        }

                        else
                        {
                            unit.Elements = fixSlash;
                        }
                    }

                    //if not just goes through the loop to check the rest of the list
                    else
                    {


                    }

                }
                course.AddUnit(unit);

            }
            return course;

        }
    }
}
