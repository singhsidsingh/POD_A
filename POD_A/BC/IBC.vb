Public Interface IBC
    'The BC class implements the following functions.

    Function add_New_Role(ByVal id_role_ibc As Integer, ByVal desc_role_ibc As String, ByVal type_role_ibc As String) As Integer

    Function add_New_Project(ByVal id_project_ibc As Integer, ByVal desc_project_ibc As String, ByVal name_project_ibc As String, ByVal createdby_project_ibc As String, ByVal datecreated_project_ibc As Date, ByVal updatedby_project_ibc As String, ByVal dateupdated_project_ibc As Date) As Integer

    Function add_New_Module(ByVal id_module_ibc As Integer, ByVal id_project_module_ibc As Integer, ByVal description_module_ibc As String, ByVal name_module_ibc As String, ByVal overview_module_ibc As String, ByVal techused_module_ibc As String, ByVal createdby_module_ibc As String, ByVal datecreated_module_ibc As Date, ByVal updatedby_module_ibc As String, ByVal dateupdated_module_ibc As Date) As Integer

    Function add_New_Employee(ByVal SSO_employee_ibc As String, ByVal empid_employee_ibc As String, ByVal name_employee_ibc As String, ByVal designation_employee_ibc As String, ByVal tcsjoiningdate_employee_ibc As Date, ByVal workcountry_employee_ibc As String, ByVal technology_employee_ibc As String, ByVal primaryroleid_employee_ibc As Integer, ByVal secroleid_employee_ibc As Integer, ByVal relevantprevexp_employee_ibc As Integer, ByVal tcsexp_employee_ibc As Integer, ByVal totalexp_employee_ibc As Integer, ByVal projectjoiningdate_employee_ibc As Date, ByVal gereexp_employee_ibc As Integer, ByVal supervisor_employee_ibc As String, ByVal primarycontactno_employee_ibc As String, ByVal seccontactno_employee_ibc As String, ByVal seat_employee_ibc As String) As Integer

    Function add_New_Weekly_Report(ByVal id_project_wr_ibc As Integer, ByVal id_module_wr_ibc As Integer, ByVal releaseid_wr_ibc As Integer, ByVal releasedescription_wr_ibc As String, ByVal noofresources_wr_ibc As Integer, ByVal resourcessoids_wr_ibc As String, ByVal sow_wr_ibc As String, ByVal comments_wr_ibc As String, ByVal status_wr_ibc As String, ByVal createdby_wr_ibc As String, ByVal datecreated_wr_ibc As Date, ByVal updatedby_wr_ibc As String, ByVal dateupdated_wr_ibc As Date) As Integer

    Function fetch_All_Roles() As DataTable

    Function fetch_All_Projects() As DataTable

    Function fetch_All_Modules() As DataTable

    Function fetch_All_Employees() As DataTable

    Function fetch_All_WeeklyReports() As DataTable

    Function fetch_Id_Name_Desc_Projects() As DataTable

    Function fetch_All_Modules_Of_Project(ByVal id_project_module_ibc As Integer) As DataTable

    Function fetch_All_Details_From_SSO(ByVal SSO_employee_ibc As String) As DataTable

    Function fetch_All_Details_From_Empid(ByVal empid_employee_ibc As String) As DataTable

    Function fetch_All_Details_From_Name(ByVal name_employee_ibc As String) As DataTable

    Function fetch_SSO_Of_Employees() As DataTable

    Function fetch_Empid_Of_Employees() As DataTable

    Function fetch_Name_Of_Employees() As DataTable

End Interface