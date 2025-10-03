using Newtonsoft.Json.Linq;
using ResillentConstruction.Models;
using ResillentConstruction.webapi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace ResillentConstruction
{
    public partial class App : Application
    {
        public static string AppName = "Him Kavach";
        public static string DB_Name = "ResilientConstruction.db";
        LanguageMasterDatabase languageMasterDatabase = new LanguageMasterDatabase();
        public static int Language = 0;
        public static List<LanguageMaster> MyLanguage = new List<LanguageMaster>();
        //public static List<DistrictMaster> districtMasterslist;
        SaveUserPreferencesDatabase saveUserPreferencesDatabase = new SaveUserPreferencesDatabase();

        public static double Latitude;
        public static double Longitude;
        public static double Accuracy;
        public static DateTime networkDateTime;
        public static List<DistrictMaster> districtMasterslist = new List<DistrictMaster>();
        public static DistrictMasterDatabase districtMasterDatabase = new DistrictMasterDatabase();
        public App()
        {
            InitializeComponent();
            // DistrictMaster And langMaster
            districtMasterslist = districtMasterDatabase.GetDistrictMaster("SELECT * FROM DistrictMaster").ToList();
            if (!districtMasterslist.Any())
            {
                insertdistrict();
            }

            //insertlanguageleys1();
            MyLanguage = languageMasterDatabase.GetLanguageMaster("Select * from LanguageMaster").ToList();
            if (!MyLanguage.Any())
            {
                insertlanguageleys1();
                MyLanguage = languageMasterDatabase.GetLanguageMaster("Select * from LanguageMaster").ToList();
            }

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Initialize root page via CreateWindow per .NET 9 guidance
            return new Window(new NavigationPage(new MainPage()));
        }

        public static void insertlanguageleys1()
        {
            try
            {
                LanguageMasterDatabase db = new LanguageMasterDatabase();
                db.DeleteLanguageMaster();
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('ऐप A', 'nav_drawer_items', 'Preferences', 'Preferences', 4);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अपनी प्राथमिकताओं की पुष्टि करें', 'confirmprefer', 'confirmprefer', 'Confirm Your Preferences', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सहेजें', 'save', 'save', 'Save', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('v', 'Version', 'Version', 'v', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हिम कवच', 'AppName', 'AppName', 'Him Kavach', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हिमाचल प्रदेश राज्य आपदा प्रबंधन प्राधिकरण (एचपीएसडीएमए), शिमला', 'deptt', 'deptt', 'Himachal Pradesh State Disaster Management Authority (HPSDMA), Shimla', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('क्या आप लॉग आउट करना चाहते हैं।', 'logoutmessage', 'logoutmessage', 'Are You Sure You Want To Logout.', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कृपया प्रतीक्षा करें ...', 'pleasewait', 'pleasewait', 'Please Wait...', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भाषा', 'language1', 'language1', 'Language', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भाषा चुनें', 'language', 'language', 'Change Language', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अंग्रेज़ी', 'language_drop_down', 'English', 'English', 0);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हिंदी', 'language_drop_down', 'Hindi', 'Hindi', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('होम', 'nav_drawer_items', 'Home', 'Home', 0);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अपडेट', 'update', 'update', 'Update', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('ताजा आंकड़ों प्राप्त नहीं कर सका', 'msg_error_service_connection', 'msg_error_service_connection', 'Could not get latest Data', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('जिला चुनें', 'district', 'district', 'Select District', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('स्तर चुनें', 'level', 'level', 'Level', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सहेजें', 'submit', 'submit', 'SUBMIT', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('जिला', 'district1', 'district1', 'District', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('खोजें', 'search', 'search', 'SEARCH', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('रियल टाइम में परिणाम फ़िल्टर करने के लिए खोज बार में टाइप करें', 'searchtext', 'searchtext', 'Type In Search Bar To Filter Results In Realtime', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('डेटा सफलतापूर्वक लोड हो गया है !', 'msg_success_updated', 'msg_success', 'Data Successfully Loaded !', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कृपया प्रतीक्षा करें...', 'please_wait', 'please_wait', 'Please wait...', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अनुसार क्रमबद्ध करें', 'sortby', 'sortby', 'Sort By', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('स्तर', 'level1', 'level1', 'Level', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('खोजें', 'textsearch', 'textsearch', 'Search', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उद्देश्य', 'purpose', 'purpose', 'Purpose', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('विभाग', 'department', 'department', 'Department', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आरंभ तिथि', 'from', 'from', 'From Date', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('समाप्ति तिथि', 'to', 'to', 'To Date', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कोई रिकॉर्ड नहीं मिला !', 'norecords', 'norecords', 'No Records Found !', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('व्यक्तिगत जानकारी', 'personalinfo', 'personalinfo', 'Personal Information', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('व्यक्तिगत जानकारी अपडेट करें', 'updatepersonalinfo', 'updatepersonalinfo', 'Update Personal Information', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('मोबाइल', 'mobileno', 'mobileno', 'Mobile', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('मोबाइल दर्ज करें', 'entmobileno', 'entmobileno', 'Enter Mobile', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('नाम', 'name', 'name', 'Name', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('नाम दर्ज करें', 'entname', 'entname', 'Enter Name', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('स्थान दर्ज करें', 'location', 'location', 'Enter Location', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('विभाग', 'department1', 'department1', 'Department', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('विभाग दर्ज करें', 'entdepartment1', 'entdepartment1', 'Enter Department', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('पदनाम', 'designation', 'designation', 'Designation', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('पदनाम दर्ज करें', 'entdesignation', 'entdesignation', 'Enter Designation', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('ई-मेल', 'email', 'email', 'E-mail', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('ई-मेल दर्ज करें', 'entemail', 'entemail', 'Enter E-mail', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('OTP प्राप्त करें', 'getotp', 'getotp', 'Get OTP', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('OTP दर्ज करें', 'enterotp', 'enterotp', 'Enter OTP', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('OTP जमा करें', 'submitotp', 'submitotp', 'Submit OTP', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('प्रतिक्रिया भेजें', 'savefeedback', 'savefeedback', 'Send Feedback', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हटाना', 'delete', 'delete', 'Delete', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अलर्ट सफलतापूर्वक हटाया गया !', 'deletesuccess', 'deletesuccess', 'Successfully Deleted', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('10 अंक मोबाइल नंबर दर्ज करें', 'tendigitmobile', 'tendigitmobile', 'Enter 10 Digit Mobile Number', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कृपया ७ या ८ या ९ संख्या के साथ शुरू होने वाला मोबाइल नंबर दर्ज करें', 'correct_mobile', 'correct_mobile', 'Please enter correct mobile number start with 7 or 8 or 9', 0);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कृपया सही नाम दर्ज करें', 'correct_name', 'correct_name', 'Please enter correct name', 0);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सही पदनाम दर्ज करें', 'correct_designation', 'correct_designation', 'Enter Correct Designation', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सही विभाग दर्ज करें', 'correct_department', 'correct_department', 'Enter Correct Department', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कृपया सही ईमेल दर्ज करें', 'correct_email', 'correct_email', 'Please enter correct email', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कृपया मोबाइल नंबर सेट करें', 'setmobile', 'setmobile', 'Kindly Set Mobile No.', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कोई इंटरनेट कनेक्शन नहीं मिला!', 'nointernet', 'nointernet', 'No Internet Connection Found !', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हमारे बारे में', 'nav_drawer_items', 'aboutus', 'About Us', 5);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('निर्माता', 'poweredby', 'poweredby', 'Powered By', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('राष्ट्रीय सूचना-विज्ञान केंद्र हिमाचल प्रदेश', 'nic', 'nic', 'National Informatics Centre Himachal Pradesh', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('मोबाइल अनुप्रयोग विकास क्षमता केन्द्र', 'mobilecenter', 'mobilecenter', 'Centre Of Competence For Mobile App Development', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('एनआईसी एचपी', 'niccontact', 'niccontact', 'NIC HP', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('वेबसाइट', 'Website', 'Website', 'Website', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कॉल करें', 'callus', 'callus', 'Call', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('ई-मेल', 'Email', 'Email', 'E-Mail', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('विकसित द्वारा', 'developedby', 'developedby', 'Developed By', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('गोपनीयता नीति', 'PrivacyPolicy', 'PrivacyPolicy', 'Privacy Policy', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अंतिम बार सिंक किया गया : ', 'lastupdate', 'lastupdate', 'Last Synced On :', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हाँ', 'yes', 'yes', 'Yes', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('रद्द करें', 'cancel', 'cancel', 'Cancel', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('नहीं', 'no', 'no', 'No', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('मोबाइल', 'Mobile', 'Mobile', 'Mobile', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('लैंडलाइन', 'Landline', 'Landline', 'Landline', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सफलतापूर्वक लोड किया गया', 'successloaded', 'successloaded', 'Successfully Loaded', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('बंद करे', 'close', 'close', 'Close', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('डैशबोर्ऑ', 'dashtype', 'dashtype', 'Dashboard', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('लॉग इन', 'login', 'login', 'LOGIN', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('लॉग आउट', 'logout', 'logout', 'LOGOUT', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उपयोगकर्ता नाम / ईमेल आईडी दर्ज करें', 'username', 'username', 'Enter User Name/ Email Id', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('पासवर्ड दर्ज करें', 'password', 'password', 'Enter Password', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('लॉगआउट', 'nav_drawer_items', 'logout', 'Logout', 6);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('लॉगिन', 'nav_drawer_items1', 'Login', 'Login', 0);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('टिप्पणियां दर्ज करें', 'remarks', 'remarks', 'Enter Remarks', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आपकी क्वेरी', 'yourquery', 'yourquery', 'Your Query', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('स्तर चुनें', 'Choose Level', 'Choose Level', 'Choose Level', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('मदद', 'nav_drawer_items1', 'help', 'Help', 7);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('केवल अक्षर की अनुमति है', 'onlyalphabets', 'onlyalphabets', 'Only Alphabets Are Allowed In ', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अनुरोध टाइमआउट।', 'RequestTimeout', 'RequestTimeout', 'Request Timeout', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सॉकेट बंद।', 'SocketClosed', 'SocketClosed', 'Socket Closed', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अपवाद।', 'Exception', 'Exception', 'Exception', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सर्वर से कनेक्ट नहीं कर पा रहे। बाद में पुन: प्रयास करें।', 'noserver', 'noserver', 'Could not connect to server. Please try again later.', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('वेब सेवा से डेटा प्राप्त करते समय त्रुटि। कृपया बाद में पुन: प्रयास करें।', 'errorservice', 'errorservice', 'Error while fetching data from Service. Please Try Again later.', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('नींव की खुदाई एवं निर्माण', 'ExcavationandConstructionofFoundation', 'ExcavationandConstructionofFoundation', 'Excavation and Construction of Foundation', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('प्लिंथ बैंड का निर्माण', 'ConstructionofPlinthBand', 'ConstructionofPlinthBand', 'Construction of Plinth Band', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सुपर स्ट्रक्चर की चिनाई', 'MasonryofSuperStructure', 'MasonryofSuperStructure', 'Masonry of Super Structure', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('दरवाज़ों और खिड़कियों के फ्रेम की मरम्मत', 'FixingofDoorsandWindowsFrames', 'FixingofDoorsandWindowsFrames', 'Fixing of Doors and Windows Frames', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('लिंटेल बैंड और सनशेड्स', 'LintelBandandSunshades', 'LintelBandandSunshades', 'Lintel Band and Sunshades', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('छत निर्माण', 'RoofConstruction', 'RoofConstruction', 'Roof Construction', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सामग्री के बारे में सामान्य जानकारी', 'GeneralInformationaboutMaterials', 'GeneralInformationaboutMaterials', 'General Information about Materials', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('औसत निर्माण लागत', 'AverageConstructionCost', 'AverageConstructionCost', 'Average Construction Cost', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('संपूर्ण दस्तावेज़ डाउनलोड करें', 'DownloadCompleteDocuments', 'DownloadCompleteDocuments', 'Download Complete Documents', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('मेरे प्रश्न', 'RaiseQuery', 'RaiseQuery', 'My Queries', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सरल उपयोग', 'Accessibility', 'Accessibility', 'Accessibility', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('साइट की पहचान', 'SiteIdentification', 'SiteIdentification', 'Site Identification', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('साइट विकास', 'SiteDevelopment', 'SiteDevelopment', 'Site Development', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('साइट ओरिएंटेशन', 'SiteOrientation', 'SiteOrientation', 'Site Orientation', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सामान्य सुझाव', 'GeneralTips', 'GeneralTips', 'General Tips', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('स्वरूप एवं अभिमुखीकरण', 'FormandOrientation', 'FormandOrientation', 'Form and Orientation', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आंतरिक योजना', 'InternalPlanning', 'InternalPlanning', 'Internal Planning', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भवन की सामान्य आवश्यकताएँ', 'requirementsofthebuilding', 'requirementsofthebuilding', 'General requirements of the building', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('दरवाजे और खिड़कियां', 'DoorsandWindows', 'DoorsandWindows', 'Doors and Windows', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('निर्माण के लिए सामान्य अभ्यास (प्रारंभिक चरण)', 'PracticesForConstruction', 'PracticesForConstruction', 'Common Practices For Construction (Initial Phase)', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('बिल्डिंग लेआउट', 'BuildingLayout', 'BuildingLayout', 'Building Layout', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सामान्य निर्देश', 'GeneralInstructions', 'GeneralInstructions', 'General Instructions', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('छत की पटिया', 'RoofSlab', 'RoofSlab', 'Roof Slab', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('लकड़ी की छत', 'WoodenRoof', 'WoodenRoof', 'Wooden Roof', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('छत रोधन', 'RoofInsulation', 'RoofInsulation', 'Roof Insulation', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सीमेंट', 'Cement', 'Cement', 'Cement', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('रेत', 'Sand', 'Sand', 'Sand', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('पत्थर/समुच्चय', 'StoneAggregate', 'StoneAggregate', 'Stone/Aggregate', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सीमेंट कंक्रीट और मोर्टार', 'CementConcreteandMortar', 'CementConcreteandMortar', 'Cement Concrete and Mortar', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('इस्पात', 'Steel', 'Steel', 'Steel', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('ईंटों', 'Bricks', 'Bricks', 'Bricks', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('लकड़ी का काम', 'WoodenWork', 'WoodenWork', 'Wooden Work', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('छत सामग्री', 'RoofMaterial', 'RoofMaterial', 'Roof Material', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('श्रेणी से संबंधित प्रश्न', 'relatedto', 'relatedto', 'Query Related To Category', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उप श्रेणी से संबंधित प्रश्न', 'relatedtosubarea', 'relatedtosubarea', 'Query Related To Sub Category', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('श्रेणी से संबंधित प्रश्न का चयन करें', 'selectrelatedto', 'selectrelatedto', 'Select Query Related To Category', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उप श्रेणी से संबंधित प्रश्न का चयन करें', 'selectrelatedtosub', 'selectrelatedtosub', 'Select Query Related To Sub Category', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उप श्रेणी का चयन करें', 'selectsubcatgeory', 'selectsubcatgeory', 'Select Sub Category For', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उठाए गए प्रश्न देखें', 'viewRaisedQueries', 'viewRaisedQueries', 'View Raised Queries', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उठाए गए प्रश्न', 'RaisedQueries', 'RaisedQueries', 'Raised Queries', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('क्वेरी आईडी', 'lbl_queryid', 'lbl_queryid', 'Query Id', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उपश्रेणी', 'lbl_subareaname', 'lbl_subareaname', 'Sub Category', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उपयोगकर्ता टिप्पणियाँ', 'lbl_userremarks', 'lbl_userremarks', 'User Remarks', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उपयोगकर्ता क्वेरी दिनांक समय', 'lbl_userquerydate', 'lbl_userquerydate', 'User Query Datetime', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('इंजीनियर का नाम', 'lbl_engineername', 'lbl_engineername', 'Engineer Name', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('इंजीनियर टिप्पणियाँ', 'lbl_engineerremarks', 'lbl_engineerremarks', 'Engineer Remarks', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('इंजीनियर प्रतिक्रिया दिनांक समय', 'lbl_engineerresponsedate', 'lbl_engineerresponsedate', 'Engineer Response Datetime', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('इंजीनियर ईमेल', 'lbl_engineeremail', 'lbl_engineeremail', 'Engineer Email', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('इंजीनियर मोबाइल', 'lbl_engineermobile', 'lbl_engineermobile', 'Engineer Mobile', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('प्रतिक्रिया लंबित', 'Pending', 'Pending', 'Response Pending', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('प्रतिक्रिया प्रदान की गई', 'Completed', 'Completed', 'Response Provided', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('इंजीनियर का चयन करें', 'SelectEngineer', 'SelectEngineer', 'Select Engineer', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('विशेषज्ञता का क्षेत्र चुनें', 'SelectAreaOfExpertise', 'SelectAreaOfExpertise', 'Select Area Of Expertise', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('छवि', 'Image', 'Image', 'Image', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उठाए गए प्रश्न विवरण खोजें', 'Searchquerydetails', 'Searchquerydetails', 'Search Raised Query Details', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सुलझे', 'Resolved', 'Resolved', 'Resolved', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अनसुलझे', 'UnResolved', 'UnResolved', 'UnResolved', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('प्रोफ़ाइल सफलतापूर्वक बनाई गई।', 'profilecreated', 'profilecreated', 'Profile Successfully Created.', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('प्रोफ़ाइल सफलतापूर्वक अपडेट की गई।', 'profileupdated', 'profileupdated', 'Profile Successfully Updated.', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('केवल संख्यात्मक वर्णों की अनुमति है', 'numeric', 'numeric', 'Only Numeric Characters Are Allowed', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('रिज़र्व', 'reserve', 'reserve', 'Reserve', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('प्रतिपुष्टि', 'Feedback', 'Feedback', 'Feedback', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('विकल्प', 'more', 'more', 'More', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('क्या आप घर बनाना चाहते हैं?', 'constructhouse', 'constructhouse', 'Do you want to construct a house?', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आप अंतर्गत आते हैं जोन : ', 'underzone', 'underzone', 'You fall under Zone : ', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('उस जिले का चयन करें जिसमें आप घर बनाना चाहते हैं', 'districthouse', 'districthouse', 'Select District in which you want to construct a house', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('निर्माण का स्थान', 'Placeofconstruction', 'Placeofconstruction', 'Place of construction', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('क्या आप निर्माण पेशेवर/पीआरआई हैं', 'constructionprofessional', 'constructionprofessional', 'Are you construction professional/PRI', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('प्रोफ़ाइल', 'profile', 'profile', 'Profile', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('दर्ज करें ', 'enter', 'enter', 'Enter ', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('चुनें ', 'select', 'select', 'Select ', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('*अनिवार्य क्षेत्र', 'mandatory', 'mandatory', '*Mandatory Fields', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आपदा प्रतिरोधी निर्माण दिशानिर्देशों की आवश्यकता', 'videoResilientConstructionGuidelines', 'videoResilientConstructionGuidelines', 'Need for Disaster Resilient Construction Guidelines', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('दिशानिर्देश', 'guidelines', 'guidelines', 'Guidelines', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आपके क्षेत्र के लिए आपदा प्रतिरोधी निर्माण के लिए दिशानिर्देश', 'yourzoneguidelines', 'yourzoneguidelines', 'Guidelines for Disaster Resilient Construction for your zone', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अन्य जोन के लिए आपदा प्रतिरोधी निर्माण हेतु दिशानिर्देश', 'otherzoneguidelines', 'otherzoneguidelines', 'Guidelines for Disaster Resilient Construction for other zones', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('निर्माण पेशेवर/पीआरआई के लिए आपदा प्रतिरोधी निर्माण के लिए दिशानिर्देश', 'priguidelines', 'priguidelines', 'Guidelines for Disaster Resilient Construction for Construction Professional/PRI', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आपका स्वागत है', 'welcome', 'welcome', 'WELCOME', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आपका क्षेत्र ', 'yourzone', 'yourzone', 'Your zone ', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('जोन चुनें', 'ChooseZone', 'ChooseZone', 'Choose Zone', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('जोन ए', 'ZoneA', 'ZoneA', 'Zone A', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('जोन बी', 'ZoneB', 'ZoneB', 'Zone B', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('जोन सी', 'ZoneC', 'ZoneC', 'Zone C', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हिम कवच', 'lbl_navigation_header', 'lbl_navigation_header', 'Him Kavach', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आपका जिला', 'yourdistrict', 'yourdistrict', 'Your District', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('मोबाइल नंबर या ई-मेल दर्ज करें', 'entemailormobile', 'entemailormobile', 'Enter Mobile No. or E-mail', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आपके', 'aspergpsen', 'aspergpsen', 'As per your GPS location, you fall under district ', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आपके जीपीएस के अनुसार, आप ', 'aspergpshi', 'aspergpshi', 'fall under district ', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES(' जिले के अंतर्गत आते हैं', 'aspergpshi1', 'aspergpshi1', 'fall under district ', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('ज़ोन के लिए मानचित्र : ', 'mapforzone', 'mapforzone', 'Map For Zone : ', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('साइट की तैयारी और चयन', 'SitePreparationandSelection', 'SitePreparationandSelection', 'Site Preparation and Selection', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('योजना दिशानिर्देश', 'PlanningGuidelines', 'PlanningGuidelines', 'Planning Guidelines', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('निर्माण श्रेणी का चयन करें', 'SelectContructionCategory', 'SelectContructionCategory', 'Select Construction Category', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आईईसी सामग्री', 'IECMaterial', 'IECMaterial', 'IEC Material', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हिमस्खलन', 'SnowAvalanche', 'SnowAvalanche', 'Snow Avalanche', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('शीत लहर', 'ColdWave', 'ColdWave', 'Cold Wave', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप', 'Earthquake', 'Earthquake', 'Earthquake', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आग', 'Fire', 'Fire', 'Fire', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('बाढ़', 'Flood', 'Flood', 'Flood', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('बिजली चमकना', 'Lightining', 'Lightining', 'Lightining', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सड़क दुर्घटना', 'RoadAccident', 'RoadAccident', 'Road Accident', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('समर्थ आईईसी', 'SamarthIEC', 'SamarthIEC', 'Samarth IEC', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('टिड्डी हमला', 'LocustAttack', 'LocustAttack', 'Locust Attack', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('खोज एवं बचाव कार्यों पर आईईसी', 'IEConSearchandRescueOperations', 'IEConSearchandRescueOperations', 'IEC on Search and Rescue Operations', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('जागरूकता सामग्री', 'AwarenessMaterial', 'AwarenessMaterial', 'Awareness Material', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('विद्यालय', 'School', 'School', 'School', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('शहरी जोखिम में कमी', 'UrbanRiskReduction', 'UrbanRiskReduction', 'Urban Risk Reduction', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('मकान की सुरक्षा', 'HouseSafety', 'HouseSafety', 'House Safety', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूस्खलन सुरक्षा', 'LandslideSafety', 'LandslideSafety', 'Landslide Safety', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('शीत लहर के दौरान क्या करें और क्या न करें-I', 'DosandDontofColdWave1', 'DosandDontofColdWave1', 'Dos and Dont''s of Cold Wave-I', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('शीत लहर के दौरान क्या करें और क्या न करें-II', 'DosandDontofColdWave2', 'DosandDontofColdWave2', 'Dos and Dont''s of Cold Wave-II', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('शीत लहर के दौरान क्या करें और क्या न करें-III', 'DosandDontofColdWave3', 'DosandDontofColdWave3', 'Dos and Dont''s of Cold Wave-III', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('शीत लहर जागरूकता', 'ColdWaveAwareness', 'ColdWaveAwareness', 'Cold Wave Awareness', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप सुरक्षा हैंग अप', 'EarthquakeSafetyHangUp', 'EarthquakeSafetyHangUp', 'Earthquake Safety Hang Up', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप तैयारी गाइड', 'EarthquakePreparednessGuide', 'EarthquakePreparednessGuide', 'Earthquake Preparedness Guide', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप से बचाव गाइड', 'EarthQuakeSurvivalGuide', 'EarthQuakeSurvivalGuide', 'Earthquake Survival Guide', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप सुरक्षा ट्राइफोल्ड', 'EarthquakeSafetyTrifold', 'EarthquakeSafetyTrifold', 'Earthquake Safety Trifold', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप सुरक्षा', 'EarthQuakeSafety', 'EarthQuakeSafety', 'Earthquake Safety', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप पोस्टर', 'EarthquakePoster', 'EarthquakePoster', 'Earthquake Poster', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कार्यस्थल पर भूकंप जागरूकता', 'EarthquakeAwarenessforworkplace', 'EarthquakeAwarenessforworkplace', 'Earthquake Awareness for workplace', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अग्नि सुरक्षा हैंग अप', 'FireSafetyHangUp', 'FireSafetyHangUp', 'FireSafetyHangUp', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('आग सुरक्षा', 'FireSafety', 'FireSafety', 'Fire Safety', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('बाढ़ सुरक्षा', 'FloodSafety', 'FloodSafety', 'Flood Safety', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('जल बचाव पुस्तिका', 'WaterRescueBooklet', 'WaterRescueBooklet', 'Water Rescue Booklet', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हिम क्षेत्र और जल पुस्तिका', 'SnowRegionandWaterBooklet', 'SnowRegionandWaterBooklet', 'Snow Region and Water Booklet', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सड़क दुर्घटना मैनुअल', 'RoadAccidentManual', 'RoadAccidentManual', 'Road Accident Manual', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूस्खलन पुस्तिका', 'LandslideBooklet', 'LandslideBooklet', 'Landslide Booklet', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('घटना प्रतिक्रिया प्रणाली', 'IncidentResponseSystem', 'IncidentResponseSystem', 'Incident Response System', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('बिजली से सुरक्षा पर फ़्लायर', 'FlyeronLightningSafety', 'FlyeronLightningSafety', 'Flyer on Lightning Safety', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('पत्रक', 'Leaflet', 'Leaflet', 'Leaflet', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सामान्य निर्देश (हिंदी)', 'GeneralInstructionHindi', 'GeneralInstructionHindi', 'General Instructions (Hindi)', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सड़क सुरक्षा त्रिफोल्ड', 'RoadSafetyTrifold', 'RoadSafetyTrifold', 'Road Safety Trifold', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सड़क सुरक्षा पर पोस्टर-II', 'PosteronRoadSafety2', 'PosteronRoadSafety2', 'Poster on Road Safety-II', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सड़क सुरक्षा पर पोस्टर-I', 'PosteronRoadSafety1', 'PosteronRoadSafety1', 'Poster on Road Safety-I', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('ग्लोफ ट्राइफोल्ड', 'GLOFTrifold', 'GLOFTrifold', 'GLOF Trifold', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('समर्थ पुस्तिका', 'SamarthBooklet', 'SamarthBooklet', 'Samarth Booklet', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('दृश्य हिमस्खलन सुरक्षा पुस्तिका', 'VisualAvalancheSafetyBooklet', 'VisualAvalancheSafetyBooklet', 'Visual Avalanche Safety Booklet', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हिमस्खलन का नियंत्रित विमोचन', 'ControlledReleaseOfAvalanche', 'ControlledReleaseOfAvalanche', 'Controlled Release Of Avalanche', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अवधव हिमसखलं निर्देशिका हिन्दी', 'AvdhavHimskhalanNirdeshikaHindi', 'AvdhavHimskhalanNirdeshikaHindi', 'Avdhav Himskhalan Nirdeshika Hindi', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('हिमस्खलन ए उपयोगकर्ता गाइड', 'AvalancheAUsersGuide', 'AvalancheAUsersGuide', 'Avalanche A Users Guide', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अवधव हिमस्खलन', 'AvadhavAvalanche', 'AvadhavAvalanche', 'Avadhav Avalanche', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('घर निर्माण सुरक्षा ट्राइफोल्ड', 'HouseConstructionSafetyTrifold', 'HouseConstructionSafetyTrifold', 'House Construction Safety Trifold', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूस्खलन सुरक्षा ट्राइफオルド', 'LandslideSafetyTrifold', 'LandslideSafetyTrifold', 'Landslide Safety Trifold', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('छात्र सड़क सुरक्षा', 'StudentsRoadSafety', 'StudentsRoadSafety', 'Students Road Safety', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('स्कूल सुरक्षा', 'SchoolSafety', 'SchoolSafety', 'School Safety', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('स्कूल अग्नि सुरक्षा', 'SchoolFireSafety', 'SchoolFireSafety', 'School Fire Safety', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('शिमला यूआरआर प्रदर्शनी', 'ShimlaURRExhibition', 'ShimlaURRExhibition', 'Shimla URR Exhibition', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('शिमला यूआरआर हैंडबिल पर आग लगी', 'ShimlaURRhandbillonFire', 'ShimlaURRhandbillonFire', 'Shimla URR Handbill on Fire', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('गैर-इंजीनियरिंग भवनों के निर्माण के लिए मेसन गाइड (द्विभाषी)', 'masonguide', 'masonguide', 'Mason''s guide for construction of Non-Engineered buildings(Bilingual)', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('गैर-इंजीनियरिंग भवनों के निर्माण के लिए स्वामी मार्गदर्शिका(हिंदी)', 'Ownersguidehindi', 'Ownersguidehini', 'Owner''s guide for construction of Non-Engineered buildings(Hindi) ', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('गैर-इंजीनियरिंग भवनों के निर्माण के लिए स्वामी मार्गदर्शिका(अंग्रेजी)', 'Ownersguideeng', 'Ownersguideeng', 'Owner''s guide for construction of Non-Engineered buildings(English)', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सुरक्षित निर्माण के लिए अधिक दिशानिर्देश', 'moreguidelines', 'moreguidelines', 'More Guidelines For Safe Construction', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सुरक्षित निर्माण के लिए पंचायत स्तर पर राजमिस्त्रियों, बढ़ईयों और तार बांधने वालों की क्षमता निर्माण हेतु रणनीति विकसित करना', 'EvolvingStrategyPanchayatlevel', 'EvolvingStrategyPanchayatlevel', 'Evolving Strategy for Capacity Building of Masons, Carpenters and Wire Binders at Panchayat level for safe construction', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सरलीकृत दिशानिर्देश क्षेत्र V', 'SimplifiedGuidelineZoneV', 'SimplifiedGuidelineZoneV', 'Simplified Guideline Zone V', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('रेट्रोफिटिंग दिशानिर्देश', 'RetrofittingGuidelines', 'RetrofittingGuidelines', 'Retrofitting Guidelines', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप रेट्रोफिटिंग के लिए गाइड', 'GuideForEarthquakeRetrofitting', 'GuideForEarthquakeRetrofitting', 'Guide For Earthquake Retrofitting', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप प्रतिरोधी निर्माण IV', 'EarthQuakeResistantConstructionIV', 'EarthQuakeResistantConstructionIV', 'Earthquake Resistant Construction IV', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंपीय क्षेत्र के लिए छोटे आवास इकाई का एक विशिष्ट डिजाइन', 'dwellingunitforSeismiczone', 'dwellingunitforSeismiczone', 'A typical design of small dwelling unit for Seismic zone', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सुरक्षा टिप्स', 'SafetyTips', 'SafetyTips', 'Safety Tips', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('शीत लहर/ठंढ', 'ColdWaveFrost', 'ColdWaveFrost', 'Cold Wave/Frost', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('शीत लहर', 'ColdWaveSafetyTips', 'ColdWaveSafetyTips', 'Cold Wave', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूस्खलन', 'LandSlidesSafetyTips', 'LandSlidesSafetyTips', 'LandSlides', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('लैंड स्लाइड रेडी रेकनर', 'LandSlideReadyReckonerSafetyTips', 'LandSlideReadyReckonerSafetyTips', 'Land Slide Ready Reckoner', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('बाढ़ सुरक्षा युक्तियाँ', 'FloodSafetyTips', 'FloodSafetyTips', 'Flood Safety Tips', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अग्नि जागरूकता', 'FireAwarenessSafetyTips', 'FireAwarenessSafetyTips', 'Fire Awareness', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप सुरक्षा युक्तियाँ', 'EarthQuakeSafetyTips', 'EarthQuakeSafetyTips', 'Earthquake Safety Tips', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('दिशानिर्देश/सुझाव डाउनलोड करें', 'Download', 'Download', 'Download Guidelines/Tips', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('इंजीनियरों के लिए आपदा प्रतिरोधी निर्माण हेतु दिशानिर्देश', 'engineerguidelines', 'engineerguidelines', 'Guidelines for Disaster Resilient Construction for Engineers', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('गुणवत्ता', 'Quality', 'Quality', 'Quality', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('अनुलग्नक', 'Annexure', 'Annexure', 'Annexure', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('परिबद्ध चिनाई', 'ConfinedMasonry', 'ConfinedMasonry', 'Confined Masonry', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('भूकंप में परिबद्ध चिनाई संरचनाओं का प्रदर्शन', 'EarthquakePerformanceofConfinedMasonryStructures', 'EarthquakePerformanceofConfinedMasonryStructures', 'Earthquake Performance of Confined Masonry Structures', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('सामुदायिक भवनों के लिए उन्नत योजनाएँ', 'ImprovisedplansforCommunityBuildings', 'ImprovisedplansforCommunityBuildings', 'Improvised plans for community buildings', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('परिबद्ध चिनाई के बारे में सामान्य जानकारी', 'MaterialSpecificationsinConfinedMasonry', 'MaterialSpecificationsinConfinedMasonry', 'Material Specifications In Confined Masonry', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('क्या करें और क्या ना करें', 'Dos', 'Dos', 'Do''s and Dont''s', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('कार्यस्थल की तैयारी', 'SitePreparation', 'SitePreparation', 'Site Preparation', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('स्थल चयन', 'SiteSelection', 'SiteSelection', 'Site Selection', 1);");
                db.ExecuteNonQuery("INSERT INTO languagemaster(LocalResourceValue, MultipleResourceKey, ResourceKey, ResourceValue, Sequence) VALUES('परिबद्ध चिनाई के साथ काम करना', 'StepsToConfinedMasonry', 'StepsToConfinedMasonry', 'Steps To Confined Masonry', 1);");


                App.MyLanguage = db.GetLanguageMaster($"select * from  LanguageMaster").ToList();


                // insert 
            }
            catch
            {

            }
        }


        public void insertdistrict()
        {
            try
            {
                DistrictMasterDatabase db = new DistrictMasterDatabase();
                //db.DeleteDistrictMaster();


                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(1, 'Bilaspur', 'बिलासपुर', 'C', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(2, 'Chamba', 'चम्बा', 'B', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(3, 'Hamirpur', 'हमीरपुर', 'C', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(4, 'Kangra', 'काँगड़ा', 'B', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(5, 'Kinnaur', 'किन्नौर', 'A', 'A');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(6, 'Kullu', 'कुल्लू', 'B', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(7, 'LAHAUL - SPITI', 'लाहौल -स्पीति ', 'A', 'A');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(8, 'MANDI', 'मंडी', 'B', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(9, 'Shimla', 'शिमला', 'B', 'B');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(10, 'Sirmaur', 'सिरमौर', 'C', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(11, 'SOLAN', 'सोलन', 'C', 'C');");
                db.ExecuteNonQuery("INSERT INTO DistrictMaster(DistrictCode, DistrictName, DistrictNameLocal,ZoneName,ZoneCode) VALUES(12, 'Una', 'ऊना', 'C', 'C');");
            }
            catch (Exception)
            {
            }
        }
        public static string LableText(string key)
        {
            try//Preferences.Get("lan", "EN-IN")
            {
                if (Preferences.Get("lan", "EN-IN") == "EN-IN")
                {
                    return App.MyLanguage.FindAll(x => (x.ResourceKey?.Trim().ToLower() ?? string.Empty) == (key?.Trim().ToLower() ?? string.Empty)).FirstOrDefault()?.ResourceValue ?? key;
                }
                else
                {
                    return App.MyLanguage.FindAll(x => (x.ResourceKey?.Trim().ToLower() ?? string.Empty) == (key?.Trim().ToLower() ?? string.Empty)).FirstOrDefault()?.LocalResourceValue ?? key;
                }
            }
            catch (Exception)
            {
                // return ex.Message;
                return key;
            }
        }

        /*    public static string LableText(string Key)
            {
                string Lable_Name = "No Value";
                try
                {
                    Lable_Name = MyLanguage.FindAll(s => s.ResourceKey == Key).ElementAt(0).ResourceValue;
                }
                catch
                {
                    Lable_Name = Key;
                }
                return Lable_Name;
            }*/

        public static string GetLableByMultipleKey(string Key)
        {
            string Lable_Name = "No Value";
            try
            {
                Lable_Name = MyLanguage.FindAll(s => s.MultipleResourceKey == Key).FirstOrDefault()?.ResourceValue ?? Key;
            }
            catch
            {
                Lable_Name = Key;
            }
            return Lable_Name;
        }

        /*   void Prefixed()
           {
               try
               {
                   var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ProfilePage)).Assembly;
                   Stream stream = assembly.GetManifestResourceStream("ResillentConstruction.languagejson.txt");
                   string MyJson = "";
                   using (var reader = new StreamReader(stream))
                   {
                       MyJson = reader.ReadToEnd();
                   }
                   JObject parsed = JObject.Parse(MyJson);
                   // languageMasterDatabase = new LanguageMasterDatabase();
                   languageMasterDatabase.DeleteLanguageMaster();

                   foreach (var pair in parsed)
                   {
                       if (pair.Key == "languagemaster")
                       {
                           var nodes = pair.Value;
                           var item = new LanguageMaster();
                           foreach (var node in nodes)
                           {
                               item.StateID = node["StateID"].ToString();
                               item.MultipleResourceKey = node["MultipleResourceKey"].ToString();
                               item.ResourceKey = node["ResourceKey"].ToString();
                               item.ResourceValue = node["ResourceValue"].ToString();
                               item.LocalResourceValue = node["LocalResourceValue"].ToString();
                               item.Sequence = node["Sequence"].ToString();

                               languageMasterDatabase.AddLanguageMaster(item);
                           }
                       }

                   }
               }
               catch (Exception ey)
               {
                   Current.MainPage.DisplayAlert("NICVC", ey.Message + "Failed to Load Default Data. Please Uninstall and then Re-install the App again", "OK");
               }

           }*/
        public static async Task GetLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);

                var location_Exact = await Geolocation.GetLocationAsync(request);
                if (location_Exact != null)
                {
                    Latitude = location_Exact.Latitude;
                    Longitude = location_Exact.Longitude;
                    Accuracy = location_Exact.Accuracy ?? 0.0;
                    DateTime locationtime = location_Exact.Timestamp.UtcDateTime.AddHours(5.5);
                    var placemarks = await Geocoding.GetPlacemarksAsync(location_Exact.Latitude, location_Exact.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    //radius = sCoord.GetDistanceTo(eCoord).ToString();
                    //App.locaddress = placemark.SubLocality;
                    networkDateTime = locationtime;
                    // networkDateTime = locationtime;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                var window = Application.Current?.Windows?.FirstOrDefault();
                if (window?.Page != null)
                    await window.Page.DisplayAlert(AppName, fnsEx.Message + "\n" + "App Cannot be used without Location", "Close");
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();

                Latitude = 0.00;
                Longitude = 0.00;
                Accuracy = 0;
                return;
            }
            catch (FeatureNotEnabledException fneEx)
            {
                var window2 = Application.Current?.Windows?.FirstOrDefault();
                bool m = false;
                if (window2?.Page != null)
                    m = await window2.Page.DisplayAlert(AppName, fneEx.Message + "\n" + "App Cannot be used without Location.",
                        "Close", "Settings");
                if (m)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
                else
                {
                    AppInfo.ShowSettingsUI();//.OpenSettings();
                                             // System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
                Latitude = 0.00;
                Longitude = Latitude;
                Accuracy = 0;
                return;
            }
            catch (PermissionException pEx)
            {
                var window3 = Application.Current?.Windows?.FirstOrDefault();
                bool m = false;
                if (window3?.Page != null)
                    m = await window3.Page.DisplayAlert(AppName, pEx.Message + "\n" + "App Cannot be used without Location Permission.",
                        "No Thanks",
                        "Settings");
                if (m)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
                else
                {

                    AppInfo.ShowSettingsUI();//.OpenSettings();
                }
                Latitude = 0.00;
                Longitude = 0.00;
                Accuracy = 0;
                return;
            }
            catch (Exception)
            {

                Latitude = 0.00;
                Longitude = Latitude;
                Accuracy = 0;
                return;
            }
        }


        public static async Task<Placemark> GetmyAddress()
        {
            try
            {
                var placeholder = await Geocoding.GetPlacemarksAsync(App.Latitude, App.Longitude);
                return placeholder?.FirstOrDefault() ?? new Placemark();
            }
            catch
            {
                return new Placemark();
            }
        }
        public static bool isAlphabetonly(string strtocheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z \s]+$");
            return rg.IsMatch(strtocheck);
        }
        public static bool isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,./_@()]*$");
            return rg.IsMatch(strToCheck);
        }

        public static bool isNumeric(string strToCheck)
        {
            Regex rg = new Regex("^[0-9]+$");
            return rg.IsMatch(strToCheck);
        }

        public static bool isvalidpassword(string strToCheck)
        {
            Regex rg = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
            return rg.IsMatch(strToCheck);
        }

        public static bool isvalidemail(string strToCheck)
        {
            Regex rg = new Regex(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z");
            return rg.IsMatch(strToCheck);
        }
        /*  public static string GetLabelByKey(string Key)
          {
              string Lable_Name = "No Value";
              try
              {
                  Lable_Name = MyLanguage.FindAll(s => s.ResourceKey == Key).ElementAt(0).ResourceValue;
              }
              catch
              {
                  Lable_Name = Key;
              }
              return Lable_Name;
          }

          public static string GetLableByMultipleKey(string Key)
          {
              string Lable_Name = "No Value";
              try
              {
                  Lable_Name = MyLanguage.FindAll(s => s.MultipleResourceKey == Key).ElementAt(0).ResourceValue;
              }
              catch
              {
                  Lable_Name = Key;
              }
              return Lable_Name;
          }*/


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
