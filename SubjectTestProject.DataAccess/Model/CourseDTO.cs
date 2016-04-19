using SubjectTestProject.DataAccess.TgaTrainingComp;
using SubjectTestProject.Domain;
using SubjectTestProject.Domain.Model;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SubjectTestProject.DataAccess.Model
{
    public class CourseDTO
    {
        private DomainDbContext db = new DomainDbContext();

        public CourseDTO()
        {
            Units = new List<UnitDTO>();
        }
        public CourseDTO(string code)
        {
            TrainingComponentServiceClient proxy = new TrainingComponentServiceClient("TrainingComponentServiceBasicHttpEndpoint");
            if (proxy.ClientCredentials != null)
            {
                proxy.ClientCredentials.UserName.UserName = "WebService.Read";
                proxy.ClientCredentials.UserName.Password = "Asdf098";
            }
            var timeCheck = DateTime.Now.Millisecond;
            var serverTime = proxy.GetServerTime();
            timeCheck = timeCheck - DateTime.Now.Millisecond;
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
            //custom add-or-update courses
            Course course = db.Courses.Include("Units").FirstOrDefault(s => s.Code == requestResult.Code);
            if (course == null)
            {
                course = new Course(requestResult.Code,requestResult.Title,requestResult.ParentCode,requestResult.ParentTitle);
                db.Courses.Add(course);
                db.SaveChanges();
            }
            else
            {
                course.Name = requestResult.Title != null?requestResult.Title:course.Name;
                course.ParentCode = requestResult.ParentCode != null?requestResult.ParentCode:course.ParentCode;
                course.ParentTitle = requestResult.ParentTitle != null?requestResult.ParentTitle:course.ParentTitle;
                if(course.Units != null)
                {
                    course.RemoveAllUnit();
                }

                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
            }
            Id = course.Id;
            Name = course.Name;
            Code = course.Code;
            //create request objects for unit codes
            TrainingComponentDetailsRequest rr = new TrainingComponentDetailsRequest()
            {
                InformationRequest = trainingComponentInformationRequestedTwo
            };
            TrainingComponent rrs = new TrainingComponent();

            //fixes the '\' in links to '/'
            string fixSlash;
            Units = new List<UnitDTO>();
            for (int i = 0; i < requestResult.Releases[0].UnitGrid.Count(); i++)
            {
                //gets unit code from main request(qualification code) array
                rr.Code = requestResult.Releases[0].UnitGrid[i].Code;

                //gets details of unit
                rrs = proxy.GetDetails(rr);

                //create unit and unitDTO object
                Unit unit = db.Units.FirstOrDefault(s => s.Code == rrs.Code);
                UnitDTO unitx = new UnitDTO();
                if(unit == null)
                {
                    unit = new Unit(rrs.Code,rrs.Title);
                    db.Units.Add(unit);
                    db.SaveChanges();
                }
                else
                {
                    unit.Name = rrs.Title != null?rrs.Title:unit.Name;
                }
                unitx.Id = unit.Id;
                unitx.Code = unit.Code;
                unitx.Name = unit.Name;
                unitx.IsEssential = requestResult.Releases[0].UnitGrid[i].IsEssential;

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
                            using (var webClient = new WebClient())
                            {
                                // download unit assessment requirements in xml string
                                try
                                {
                                    unit.AssessmentRequirements = webClient.DownloadString(fixSlash);
                                }
                                catch (WebException)
                                {
                                }
                            }
                        }

                        else
                        {
                            using (var webClient = new WebClient())
                            {
                                //download unit element in xml string
                                try
                                {
                                    unit.Elements = webClient.DownloadString(fixSlash);
                                }
                                catch (WebException)
                                {
                                }
                            }
                        }
                    }

                    //if not just goes through the loop to check the rest of the list
                    else
                    {


                    }

                }
                db.Entry(unit).State = EntityState.Modified;
                db.SaveChanges();
                CourseUnit cunit = new CourseUnit();
                cunit.Unit = unit;
                cunit.IsEssential = requestResult.Releases[0].UnitGrid[i].IsEssential;
                course.Units.Add(cunit);
                //course.AddUnit(unit);
                Units.Add(unitx);
            }
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<UnitDTO> Units { get; set; }
    }
}
