'An alternative for System.Data.OracleClient must be found.
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Data
Imports System.Data.OracleClient

''' <summary>
''' This class is where contact is established and data exchanged with the back end.
''' </summary>
''' <remarks></remarks>
Public Class BC
    Inherits CONSTANTS
    Implements IBC
    'CONSTANTS contains the names of the back end procedures.
    'IBC is the interface which is implemented by this class.

    'Must find alternatives for OracleConnection and OracleCommand.
    '(They are deprecated, as can be seen below.)
    Private connection As OracleConnection = Nothing
    Private command As OracleCommand = Nothing
    Private backend_error_msg As String = ""

    ''' <summary>
    ''' Constructor overloading for BC class takes connection string as
    ''' its input parameter and initializes the connection with it.
    ''' </summary>
    ''' <param name="connection_string_bc"></param>
    ''' <remarks></remarks>
    Sub New(ByVal connection_string_bc As String)
        Try
            'Initializing connection with the database using the provided connection string.
            connection = New OracleConnection(connection_string_bc)
        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Function to add a new role.
    ''' </summary>
    ''' <param name="id_role_bc"></param>
    ''' <param name="desc_role_bc"></param>
    ''' <param name="type_role_bc"></param>
    ''' <returns>Returns 1 if operation is successful, 0 otherwise.</returns>
    ''' <remarks></remarks>
    Function add_New_Role(ByVal id_role_bc As Integer, ByVal desc_role_bc As String, ByVal type_role_bc As String) As Integer Implements IBC.add_New_Role
        Try
            'Declaring an array of OracleParameter objects which will transport our values to the back end.
            Dim lobjParam(3) As OracleParameter

            'The following will capture the return value of Execute.NonQuery.
            Dim no_of_rows_affected As Integer = 0

            'The following will specify a unique id for the role being added.
            lobjParam(0) = New OracleParameter("p_in_id_role", OracleType.Number)
            lobjParam(0).Value = id_role_bc
            lobjParam(0).Direction = ParameterDirection.Input

            'The following will specify a description of the role being added.
            lobjParam(1) = New OracleParameter("p_in_desc_role", OracleType.VarChar)
            lobjParam(1).Value = desc_role_bc
            lobjParam(1).Direction = ParameterDirection.Input

            'The following will specify the type of the role being added.
            lobjParam(2) = New OracleParameter("p_in_type_role", OracleType.VarChar)
            lobjParam(2).Value = type_role_bc
            lobjParam(2).Direction = ParameterDirection.Input

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(3) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(3).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(3).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_ADD_NEW_ROLE, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Executing the command.
            no_of_rows_affected = command.ExecuteNonQuery()

            'Checking if any error occured in the back end.
            If lobjParam(3).Value Is DBNull.Value Or lobjParam(3).Value.ToString().Equals("") Then
                'No error. Now check the number of rows affected by the query/procedure.
                If no_of_rows_affected <> 0 Then
                    'Atleast something happened. Return 1 to signal success.
                    Return 1
                Else
                    'No error occurred in the back end; but no rows were affected as well.
                    'This requires deeper investigation. Return 0 to signal failure.
                    Return 0
                End If
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(3).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to add a new project.
    ''' </summary>
    ''' <param name="id_project_bc"></param>
    ''' <param name="desc_project_bc"></param>
    ''' <param name="name_project_bc"></param>
    ''' <param name="createdby_project_bc"></param>
    ''' <param name="datecreated_project_bc"></param>
    ''' <param name="updatedby_project_bc"></param>
    ''' <param name="dateupdated_project_bc"></param>
    ''' <returns>Returns 1 if operation is successful, 0 otherwise.</returns>
    ''' <remarks></remarks>
    Function add_New_Project(ByVal id_project_bc As Integer, ByVal desc_project_bc As String, ByVal name_project_bc As String, ByVal createdby_project_bc As String, ByVal datecreated_project_bc As Date, ByVal updatedby_project_bc As String, ByVal dateupdated_project_bc As Date) As Integer Implements IBC.add_New_Project
        Try
            'Declaring an array of OracleParameter objects which will transport our values to the back end.
            Dim lobjParam(7) As OracleParameter

            'The following will capture the return value of Execute.NonQuery.
            Dim no_of_rows_affected As Integer = 0

            'The following will specify a unique id for the project being added.
            lobjParam(0) = New OracleParameter("p_in_id_project", OracleType.Number)
            lobjParam(0).Value = id_project_bc
            lobjParam(0).Direction = ParameterDirection.Input

            'The following will specify a name for the project being added.
            lobjParam(1) = New OracleParameter("p_in_name_project", OracleType.VarChar)
            lobjParam(1).Value = name_project_bc
            lobjParam(1).Direction = ParameterDirection.Input

            'The following will specify a description of the project being added.
            lobjParam(2) = New OracleParameter("p_in_desc_project", OracleType.VarChar)
            lobjParam(2).Value = desc_project_bc
            lobjParam(2).Direction = ParameterDirection.Input

            'The following will identify the person who is adding the project.
            lobjParam(3) = New OracleParameter("p_in_createdby_project", OracleType.VarChar)
            lobjParam(3).Value = createdby_project_bc
            lobjParam(3).Direction = ParameterDirection.Input

            'The following will specify the date on which the project is being added.
            lobjParam(4) = New OracleParameter("p_in_datecreated_project", OracleType.DateTime)
            lobjParam(4).Value = datecreated_project_bc
            lobjParam(4).Direction = ParameterDirection.Input

            'The following will identify the person who will update the details of the project.
            'Since this particular function will be used to add a *new* project, it is obvious
            'that the project will not have been "updated" as of yet.
            'Therefore, let's place some default placeholder values here which will be 
            'updated in the function which will *actually* update the project details.
            lobjParam(5) = New OracleParameter("p_in_updatedby_project", OracleType.VarChar)
            lobjParam(5).Value = updatedby_project_bc
            lobjParam(5).Direction = ParameterDirection.Input

            'The comments for the previous parameter are valid for the following as well.
            'The following will be used to specify the date on which the project details are being updated.
            lobjParam(6) = New OracleParameter("p_in_dateupdated_project", OracleType.VarChar)
            lobjParam(6).Value = dateupdated_project_bc
            lobjParam(6).Direction = ParameterDirection.Input

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(7) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(7).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(7).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_ADD_NEW_PROJECT, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Executing the command.
            no_of_rows_affected = command.ExecuteNonQuery()

            'Checking if any error occured in the back end.
            If lobjParam(7).Value Is DBNull.Value Or lobjParam(7).Value.ToString().Equals("") Then
                'No error. Now check the number of rows affected by the query/procedure.
                If no_of_rows_affected <> 0 Then
                    'Atleast something happened. Return 1 to signal success.
                    Return 1
                Else
                    'No error occurred in the back end; but no rows were affected as well.
                    'This requires deeper investigation. Return 0 to signal failure.
                    Return 0
                End If
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(7).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to add a new module.
    ''' </summary>
    ''' <param name="id_module_bc"></param>
    ''' <param name="id_project_module_bc"></param>
    ''' <param name="description_module_bc"></param>
    ''' <param name="name_module_bc"></param>
    ''' <param name="overview_module_bc"></param>
    ''' <param name="techused_module_bc"></param>
    ''' <param name="createdby_module_bc"></param>
    ''' <param name="datecreated_module_bc"></param>
    ''' <param name="updatedby_module_bc"></param>
    ''' <param name="dateupdated_module_bc"></param>
    ''' <returns>Returns 1 if operation is successful, 0 otherwise.</returns>
    ''' <remarks></remarks>
    Function add_New_Module(ByVal id_module_bc As Integer, ByVal id_project_module_bc As Integer, ByVal description_module_bc As String, ByVal name_module_bc As String, ByVal overview_module_bc As String, ByVal techused_module_bc As String, ByVal createdby_module_bc As String, ByVal datecreated_module_bc As Date, ByVal updatedby_module_bc As String, ByVal dateupdated_module_bc As Date) As Integer Implements IBC.add_New_Module
        Try
            'Declaring an array of OracleParameter objects which will transport our values to the back end.
            Dim lobjParam(10) As OracleParameter

            'The following will capture the return value of Execute.NonQuery.
            Dim no_of_rows_affected As Integer = 0

            'The following will specify a unique id for the module being added.
            lobjParam(0) = New OracleParameter("p_in_id_module", OracleType.Number)
            lobjParam(0).Value = id_module_bc
            lobjParam(0).Direction = ParameterDirection.Input

            'The following will specify the id of the project to which the module belongs.
            lobjParam(1) = New OracleParameter("p_in_id_project_module", OracleType.Number)
            lobjParam(1).Value = id_project_module_bc
            lobjParam(1).Direction = ParameterDirection.Input

            'The following will specify the name of the module being added.
            lobjParam(2) = New OracleParameter("p_in_name_module", OracleType.VarChar)
            lobjParam(2).Value = name_module_bc
            lobjParam(2).Direction = ParameterDirection.Input

            'The following will specify a description of the module being added.
            lobjParam(3) = New OracleParameter("p_in_description_module", OracleType.VarChar)
            lobjParam(3).Value = description_module_bc
            lobjParam(3).Direction = ParameterDirection.Input

            'The following will specify a brief overview of the module being added.
            lobjParam(4) = New OracleParameter("p_in_overview_module", OracleType.VarChar)
            lobjParam(4).Value = overview_module_bc
            lobjParam(4).Direction = ParameterDirection.Input

            'The following will specify the technology being used in the module being added.
            lobjParam(5) = New OracleParameter("p_in_techused_module", OracleType.VarChar)
            lobjParam(5).Value = techused_module_bc
            lobjParam(5).Direction = ParameterDirection.Input

            'The following will identify the user who is adding the module.
            lobjParam(6) = New OracleParameter("p_in_createdby_module", OracleType.VarChar)
            lobjParam(6).Value = createdby_module_bc
            lobjParam(6).Direction = ParameterDirection.Input

            'The following will specify the date on which the module is being added.
            lobjParam(7) = New OracleParameter("p_in_datecreated_module", OracleType.DateTime)
            lobjParam(7).Value = datecreated_module_bc
            lobjParam(7).Direction = ParameterDirection.Input

            'The following will identify the person who will update the details of the module.
            'Since this particular function will be used to add a *new* module, it is obvious
            'that the module will not have been "updated" as of yet.
            'Therefore, let's place some default placeholder values here which will be 
            'updated in the function which will *actually* update the module details.
            lobjParam(8) = New OracleParameter("p_in_updatedby_module", OracleType.VarChar)
            lobjParam(8).Value = updatedby_module_bc
            lobjParam(8).Direction = ParameterDirection.Input

            'The comments for the previous parameter are valid for the following as well.
            'The following will be used to specify the date on which the module details are being updated.
            lobjParam(9) = New OracleParameter("p_in_dateupdated_module", OracleType.VarChar)
            lobjParam(9).Value = dateupdated_module_bc
            lobjParam(9).Direction = ParameterDirection.Input

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(10) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(10).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(10).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_ADD_NEW_MODULE, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Executing the command.
            no_of_rows_affected = command.ExecuteNonQuery()

            'Checking if any error occured in the back end.
            If lobjParam(10).Value Is DBNull.Value Or lobjParam(10).Value.ToString().Equals("") Then
                'No error. Now check the number of rows affected by the query/procedure.
                If no_of_rows_affected <> 0 Then
                    'Atleast something happened. Return 1 to signal success.
                    Return 1
                Else
                    'No error occurred in the back end; but no rows were affected as well.
                    'This requires deeper investigation. Return 0 to signal failure.
                    Return 0
                End If
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(10).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to add a new employee.
    ''' </summary>
    ''' <param name="SSO_employee_bc"></param>
    ''' <param name="empid_employee_bc"></param>
    ''' <param name="name_employee_bc"></param>
    ''' <param name="designation_employee_bc"></param>
    ''' <param name="tcsjoiningdate_employee_bc"></param>
    ''' <param name="workcountry_employee_bc"></param>
    ''' <param name="technology_employee_bc"></param>
    ''' <param name="primaryroleid_employee_bc"></param>
    ''' <param name="secroleid_employee_bc"></param>
    ''' <param name="relevantprevexp_employee_bc"></param>
    ''' <param name="tcsexp_employee_bc"></param>
    ''' <param name="totalexp_employee_bc"></param>
    ''' <param name="projectjoiningdate_employee_bc"></param>
    ''' <param name="gereexp_employee_bc"></param>
    ''' <param name="supervisor_employee_bc"></param>
    ''' <param name="primarycontactno_employee_bc"></param>
    ''' <param name="seccontactno_employee_bc"></param>
    ''' <param name="seat_employee_bc"></param>
    ''' <returns>Returns 1 if operation is successful, 0 otherwise.</returns>
    ''' <remarks></remarks>
    Function add_New_Employee(ByVal SSO_employee_bc As String, ByVal empid_employee_bc As String, ByVal name_employee_bc As String, ByVal designation_employee_bc As String, ByVal tcsjoiningdate_employee_bc As Date, ByVal workcountry_employee_bc As String, ByVal technology_employee_bc As String, ByVal primaryroleid_employee_bc As Integer, ByVal secroleid_employee_bc As Integer, ByVal relevantprevexp_employee_bc As Integer, ByVal tcsexp_employee_bc As Integer, ByVal totalexp_employee_bc As Integer, ByVal projectjoiningdate_employee_bc As Date, ByVal gereexp_employee_bc As Integer, ByVal supervisor_employee_bc As String, ByVal primarycontactno_employee_bc As String, ByVal seccontactno_employee_bc As String, ByVal seat_employee_bc As String) As Integer Implements IBC.add_New_Employee
        Try
            'Declaring an array of OracleParameter objects which will transport our values to the back end.
            Dim lobjParam(18) As OracleParameter

            'The following will capture the return value of Execute.NonQuery.
            Dim no_of_rows_affected As Integer = 0

            'The following will specify the SSO (Single Sign-On) id of the employee being added.
            lobjParam(0) = New OracleParameter("p_in_SSO_employee", OracleType.VarChar)
            lobjParam(0).Value = SSO_employee_bc
            lobjParam(0).Direction = ParameterDirection.Input

            'The following will specify the TCS employee id of the employee being added.
            lobjParam(1) = New OracleParameter("p_in_empid_employee", OracleType.VarChar)
            lobjParam(1).Value = empid_employee_bc
            lobjParam(1).Direction = ParameterDirection.Input

            'The following will specify the name of the employee being added.
            lobjParam(2) = New OracleParameter("p_in_name_employee", OracleType.VarChar)
            lobjParam(2).Value = name_employee_bc
            lobjParam(2).Direction = ParameterDirection.Input

            'The following will specify the designation of the employee being added.
            lobjParam(3) = New OracleParameter("p_in_designation_employee", OracleType.VarChar)
            lobjParam(3).Value = designation_employee_bc
            lobjParam(3).Direction = ParameterDirection.Input

            'The following will specify the TCS joining date of the employee being added.
            lobjParam(4) = New OracleParameter("p_in_tcsjoiningdate_employee", OracleType.DateTime)
            lobjParam(4).Value = tcsjoiningdate_employee_bc
            lobjParam(4).Direction = ParameterDirection.Input

            'The following will specify the work country of the employee being added.
            lobjParam(5) = New OracleParameter("p_in_workcountry_employee", OracleType.VarChar)
            lobjParam(5).Value = workcountry_employee_bc
            lobjParam(5).Direction = ParameterDirection.Input

            'The following will specify the technology of the employee being added.
            lobjParam(6) = New OracleParameter("p_in_technology_employee", OracleType.VarChar)
            lobjParam(6).Value = technology_employee_bc
            lobjParam(6).Direction = ParameterDirection.Input

            'The following will specify the primary role id of the employee being added.
            lobjParam(7) = New OracleParameter("p_in_primaryroleid_employee", OracleType.Number)
            lobjParam(7).Value = primaryroleid_employee_bc
            lobjParam(7).Direction = ParameterDirection.Input

            'The following will specify the secondary role id of the employee being added.
            lobjParam(8) = New OracleParameter("p_in_secroleid_employee", OracleType.Number)
            lobjParam(8).Value = secroleid_employee_bc
            lobjParam(8).Direction = ParameterDirection.Input

            'The following will specify the relevant previous experience of the employee being added.
            lobjParam(9) = New OracleParameter("p_in_relevantprevexp_employee", OracleType.Number)
            lobjParam(9).Value = relevantprevexp_employee_bc
            lobjParam(9).Direction = ParameterDirection.Input

            'The following will specify the TCS experience of the employee being added.
            lobjParam(10) = New OracleParameter("p_in_tcsexp_employee", OracleType.Number)
            lobjParam(10).Value = tcsexp_employee_bc
            lobjParam(10).Direction = ParameterDirection.Input

            'The following will specify the total experience of the employee being added.
            lobjParam(11) = New OracleParameter("p_in_totalexp_employee", OracleType.Number)
            lobjParam(11).Value = totalexp_employee_bc
            lobjParam(11).Direction = ParameterDirection.Input

            'The following will specify the project joining date of the employee being added.
            lobjParam(12) = New OracleParameter("p_in_projectjoiningdate_employee", OracleType.DateTime)
            lobjParam(12).Value = projectjoiningdate_employee_bc
            lobjParam(12).Direction = ParameterDirection.Input

            'The following will specify the GE-RE experience of the employee being added.
            lobjParam(13) = New OracleParameter("p_in_gereexp_employee", OracleType.Number)
            lobjParam(13).Value = gereexp_employee_bc
            lobjParam(13).Direction = ParameterDirection.Input

            'The following will specify the supervisor of the employee being added.
            lobjParam(14) = New OracleParameter("p_in_supervisor_employee", OracleType.VarChar)
            lobjParam(14).Value = supervisor_employee_bc
            lobjParam(14).Direction = ParameterDirection.Input

            'The following will specify the primary contact number of the employee being added.
            lobjParam(15) = New OracleParameter("p_in_primarycontactno_employee", OracleType.VarChar)
            lobjParam(15).Value = primarycontactno_employee_bc
            lobjParam(15).Direction = ParameterDirection.Input

            'The following will specify the secondary contact number of the employee being added.
            lobjParam(16) = New OracleParameter("p_in_seccontactno_employee", OracleType.VarChar)
            lobjParam(16).Value = seccontactno_employee_bc
            lobjParam(16).Direction = ParameterDirection.Input

            'The following will specify the seat of the employee being added.
            lobjParam(17) = New OracleParameter("p_in_seat_employee", OracleType.VarChar)
            lobjParam(17).Value = seat_employee_bc
            lobjParam(17).Direction = ParameterDirection.Input

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(18) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(18).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(18).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_ADD_NEW_EMPLOYEE, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Executing the command.
            no_of_rows_affected = command.ExecuteNonQuery()

            'Checking if any error occured in the back end.
            If lobjParam(18).Value Is DBNull.Value Or lobjParam(18).Value.ToString().Equals("") Then
                'No error. Now check the number of rows affected by the query/procedure.
                If no_of_rows_affected <> 0 Then
                    'Atleast something happened. Return 1 to signal success.
                    Return 1
                Else
                    'No error occurred in the back end; but no rows were affected as well.
                    'This requires deeper investigation. Return 0 to signal failure.
                    Return 0
                End If
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(18).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to add a new weekly report.
    ''' </summary>
    ''' <param name="id_project_wr_bc"></param>
    ''' <param name="id_module_wr_bc"></param>
    ''' <param name="releaseid_wr_bc"></param>
    ''' <param name="releasedescription_wr_bc"></param>
    ''' <param name="noofresources_wr_bc"></param>
    ''' <param name="resourcessoids_wr_bc"></param>
    ''' <param name="sow_wr_bc"></param>
    ''' <param name="comments_wr_bc"></param>
    ''' <param name="status_wr_bc"></param>
    ''' <param name="createdby_wr_bc"></param>
    ''' <param name="datecreated_wr_bc"></param>
    ''' <param name="updatedby_wr_bc"></param>
    ''' <param name="dateupdated_wr_bc"></param>
    ''' <returns>Returns 1 if operation is successful, 0 otherwise.</returns>
    ''' <remarks></remarks>
    Function add_New_Weekly_Report(ByVal id_project_wr_bc As Integer, ByVal id_module_wr_bc As Integer, ByVal releaseid_wr_bc As Integer, ByVal releasedescription_wr_bc As String, ByVal noofresources_wr_bc As Integer, ByVal resourcessoids_wr_bc As String, ByVal sow_wr_bc As String, ByVal comments_wr_bc As String, ByVal status_wr_bc As String, ByVal createdby_wr_bc As String, ByVal datecreated_wr_bc As Date, ByVal updatedby_wr_bc As String, ByVal dateupdated_wr_bc As Date) As Integer Implements IBC.add_New_Weekly_Report
        Try
            'Declaring an array of OracleParameter objects which will transport our values to the back end.
            Dim lobjParam(13) As OracleParameter

            'The following will capture the return value of Execute.NonQuery.
            Dim no_of_rows_affected As Integer = 0

            'The following will specify the id of the project for which weekly report is being added.
            lobjParam(0) = New OracleParameter("p_in_id_project_wr", OracleType.Number)
            lobjParam(0).Value = id_project_wr_bc
            lobjParam(0).Direction = ParameterDirection.Input

            'The following will specify the id of the module for which weekly report is being added.
            lobjParam(1) = New OracleParameter("p_in_id_module_wr", OracleType.Number)
            lobjParam(1).Value = id_module_wr_bc
            lobjParam(1).Direction = ParameterDirection.Input

            'The following will specify the unique release id of the release for which weekly report is being added.
            lobjParam(2) = New OracleParameter("p_in_releaseid_wr", OracleType.Number)
            lobjParam(2).Value = releaseid_wr_bc
            lobjParam(2).Direction = ParameterDirection.Input

            'The following will specify the description of the release for which weekly report is being added.
            lobjParam(3) = New OracleParameter("p_in_releasedescription_wr", OracleType.VarChar)
            lobjParam(3).Value = releasedescription_wr_bc
            lobjParam(3).Direction = ParameterDirection.Input

            'The following will specify the number of resources of the release for which weekly report is being added.
            lobjParam(4) = New OracleParameter("p_in_noofresources_wr", OracleType.Number)
            lobjParam(4).Value = noofresources_wr_bc
            lobjParam(4).Direction = ParameterDirection.Input

            'The following will specify the SSO ids of the resources of the release for which weekly report is being added.
            lobjParam(5) = New OracleParameter("p_in_resourcessoids_wr", OracleType.VarChar)
            lobjParam(5).Value = resourcessoids_wr_bc
            lobjParam(5).Direction = ParameterDirection.Input

            'The following will specify the "SOW" (or is it "SWON"?) of the release for which weekly report is being added.
            lobjParam(6) = New OracleParameter("p_in_sow_wr", OracleType.VarChar)
            lobjParam(6).Value = sow_wr_bc
            lobjParam(6).Direction = ParameterDirection.Input

            'The following will specify the comments of the release for which weekly report is being added.
            lobjParam(7) = New OracleParameter("p_in_comments_wr", OracleType.VarChar)
            lobjParam(7).Value = comments_wr_bc
            lobjParam(7).Direction = ParameterDirection.Input

            'The following will specify the status of the release for which weekly report is being added.
            lobjParam(8) = New OracleParameter("p_in_status_wr", OracleType.VarChar)
            lobjParam(8).Value = status_wr_bc
            lobjParam(8).Direction = ParameterDirection.Input

            'The following will identify the person who is adding the weekly report.
            lobjParam(9) = New OracleParameter("p_in_createdby_wr", OracleType.VarChar)
            lobjParam(9).Value = createdby_wr_bc
            lobjParam(9).Direction = ParameterDirection.Input

            'The following will specify the date on which the weekly report was added.
            lobjParam(10) = New OracleParameter("p_in_datecreated_wr", OracleType.DateTime)
            lobjParam(10).Value = datecreated_wr_bc
            lobjParam(10).Direction = ParameterDirection.Input

            'The following will identify the person who will update the details of the weekly report.
            'Since this particular function will be used to add a *new* weekly report, it is obvious
            'that the weekly report will not have been "updated" as of yet.
            'Therefore, let's place some default placeholder values here which will be 
            'updated in the function which will *actually* update the weekly report details.
            lobjParam(11) = New OracleParameter("p_in_updatedby_module", OracleType.VarChar)
            lobjParam(11).Value = updatedby_wr_bc
            lobjParam(11).Direction = ParameterDirection.Input

            'The comments for the previous parameter are valid for the following as well.
            'The following will be used to specify the date on which the weekly report details are being updated.
            lobjParam(12) = New OracleParameter("p_in_dateupdated_module", OracleType.VarChar)
            lobjParam(12).Value = dateupdated_wr_bc
            lobjParam(12).Direction = ParameterDirection.Input

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(13) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(13).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(13).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_ADD_NEW_WEEKLYREPORT, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Executing the command.
            no_of_rows_affected = command.ExecuteNonQuery()

            'Checking if any error occured in the back end.
            If lobjParam(13).Value Is DBNull.Value Or lobjParam(13).Value.ToString().Equals("") Then
                'No error. Now check the number of rows affected by the query/procedure.
                If no_of_rows_affected <> 0 Then
                    'Atleast something happened. Return 1 to signal success.
                    Return 1
                Else
                    'No error occurred in the back end; but no rows were affected as well.
                    'This requires deeper investigation. Return 0 to signal failure.
                    Return 0
                End If
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(13).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all details of all the roles.
    ''' </summary>
    ''' <returns>Datatable containing details of all the roles is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_All_Roles() As DataTable Implements IBC.fetch_All_Roles
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(1) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will fetch the cursor containing all the roles.
            lobjParam(0) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(0).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(1) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(1).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(1).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_ALL_ROLES, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(1).Value Is DBNull.Value Or lobjParam(1).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(1).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all details of all the projects.
    ''' </summary>
    ''' <returns>Datatable containing details of all the projects is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_All_Projects() As DataTable Implements IBC.fetch_All_Projects
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(1) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will fetch the cursor containing all the roles.
            lobjParam(0) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(0).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(1) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(1).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(1).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_ALL_PROJECTS, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(1).Value Is DBNull.Value Or lobjParam(1).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(1).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all details of all the modules.
    ''' </summary>
    ''' <returns>Datatable containing details of all the modules is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_All_Modules() As DataTable Implements IBC.fetch_All_Modules
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(1) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will fetch the cursor containing all the roles.
            lobjParam(0) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(0).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(1) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(1).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(1).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_ALL_MODULES, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(1).Value Is DBNull.Value Or lobjParam(1).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(1).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all details of all the employees.
    ''' </summary>
    ''' <returns>Datatable containing details of all the employees is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_All_Employees() As DataTable Implements IBC.fetch_All_Employees
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(1) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will fetch the cursor containing all the roles.
            lobjParam(0) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(0).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(1) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(1).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(1).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_ALL_EMPLOYEES, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(1).Value Is DBNull.Value Or lobjParam(1).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(1).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all details of all the weekly reports.
    ''' </summary>
    ''' <returns>Datatable containing details of all the weekly reports is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_All_WeeklyReports() As DataTable Implements IBC.fetch_All_WeeklyReports
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(1) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will fetch the cursor containing all the roles.
            lobjParam(0) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(0).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(1) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(1).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(1).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_ALL_WEEKLYREPORTS, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(1).Value Is DBNull.Value Or lobjParam(1).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(1).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch the ids', names and descriptions of all projects.
    ''' </summary>
    ''' <returns>Datatable containing ids', names and descriptions of all the projects is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_Id_Name_Desc_Projects() As DataTable Implements IBC.fetch_Id_Name_Desc_Projects
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(1) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will fetch the cursor containing all the roles.
            lobjParam(0) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(0).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(1) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(1).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(1).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_ID_NAME_DESC_OF_PROJECTS, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(1).Value Is DBNull.Value Or lobjParam(1).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(1).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all the details of all the modules of a particular project.
    ''' </summary>
    ''' <param name="id_project_module_bc"></param>
    ''' <returns>Datatable containing all the details of all the modules of a particular project is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_All_Modules_Of_Project(ByVal id_project_module_bc As Integer) As DataTable Implements IBC.fetch_All_Modules_Of_Project
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(2) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will specify the id of the project for which the details of the modules are being fetched.
            lobjParam(0) = New OracleParameter("id_project_module", OracleType.Number)
            lobjParam(0).Value = id_project_module_bc
            lobjParam(0).Direction = ParameterDirection.Input

            'The following will fetch the cursor containing all the roles.
            lobjParam(1) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(1).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(2) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(2).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(2).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_ALL_MODULES_OF_PROJECT, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(2).Value Is DBNull.Value Or lobjParam(2).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(2).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all of the details of one particular employee, identified by his SSO.
    ''' </summary>
    ''' <param name="SSO_employee_bc"></param>
    ''' <returns>Datatable containing all the details of a particular employee is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_All_Details_From_SSO(ByVal SSO_employee_bc As String) As DataTable Implements IBC.fetch_All_Details_From_SSO
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(2) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will specify the SSO id of the employee whose details are to be fetched.
            lobjParam(0) = New OracleParameter("SSO_employee", OracleType.Number)
            lobjParam(0).Value = SSO_employee_bc
            lobjParam(0).Direction = ParameterDirection.Input

            'The following will fetch the cursor containing all the roles.
            lobjParam(1) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(1).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(2) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(2).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(2).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_ALL_DETAILS_OF_EMPLOYEE_FROM_SSO, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(2).Value Is DBNull.Value Or lobjParam(2).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(2).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all of the details of one particular employee, identified by his Employee ID.
    ''' </summary>
    ''' <param name="empid_employee_bc"></param>
    ''' <returns>Datatable containing all the details of a particular employee are returned.</returns>
    ''' <remarks></remarks>
    Function fetch_All_Details_From_Empid(ByVal empid_employee_bc As String) As DataTable Implements IBC.fetch_All_Details_From_Empid
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(2) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will specify the employee id of the employee whose details are to be fetched.
            lobjParam(0) = New OracleParameter("empid_employee", OracleType.Number)
            lobjParam(0).Value = empid_employee_bc
            lobjParam(0).Direction = ParameterDirection.Input

            'The following will fetch the cursor containing all the roles.
            lobjParam(1) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(1).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(2) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(2).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(2).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_ALL_DETAILS_OF_EMPLOYEE_FROM_EMPID, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(2).Value Is DBNull.Value Or lobjParam(2).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(2).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all of the details of one particular employee, identified by his name.
    ''' </summary>
    ''' <param name="name_employee_bc"></param>
    ''' <returns>Datatable containing all the details of a particular employee are returned.</returns>
    ''' <remarks></remarks>
    Function fetch_All_Details_From_Name(ByVal name_employee_bc As String) As DataTable Implements IBC.fetch_All_Details_From_Name
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(2) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will specify the name of the employee whose details are to be fetched.
            lobjParam(0) = New OracleParameter("name_employee", OracleType.Number)
            lobjParam(0).Value = name_employee_bc
            lobjParam(0).Direction = ParameterDirection.Input

            'The following will fetch the cursor containing all the roles.
            lobjParam(1) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(1).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(2) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(2).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(2).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_ALL_DETAILS_OF_EMPLOYEE_FROM_NAME, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(2).Value Is DBNull.Value Or lobjParam(2).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(2).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all of the SSOs in the system.
    ''' </summary>
    ''' <returns>Datatable containing all SSOs stored in the back end is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_SSO_Of_Employees() As DataTable Implements IBC.fetch_SSO_Of_Employees
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(1) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will fetch the cursor containing all the SSO ids.
            lobjParam(0) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(0).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(1) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(1).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(1).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_SSO_OF_EMPLOYEES, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(1).Value Is DBNull.Value Or lobjParam(1).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(1).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all of the employee ids' in the system.
    ''' </summary>
    ''' <returns>Datatable containing all employee ids' stored in the back end is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_Empid_Of_Employees() As DataTable Implements IBC.fetch_Empid_Of_Employees
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(1) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will fetch the cursor containing all the employee ids.
            lobjParam(0) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(0).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(1) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(1).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(1).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_EMPID_OF_EMPLOYEES, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(1).Value Is DBNull.Value Or lobjParam(1).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(1).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

    ''' <summary>
    ''' Function to fetch all of the employee names in the system.
    ''' </summary>
    ''' <returns>Datatable containing all employee names stored in the back end is returned.</returns>
    ''' <remarks></remarks>
    Function fetch_Name_Of_Employees() As DataTable Implements IBC.fetch_Name_Of_Employees
        Try
            'Declaring an array of OracleParameter objects which will transport/fetch our values to/from the back end.
            Dim lobjParam(1) As OracleParameter

            'The following will capture the cursor being returned from the back end.
            Dim dt As DataTable = New DataTable()

            'The following will fetch the cursor containing all the employee names.
            lobjParam(0) = New OracleParameter("p_out_data", OracleType.Cursor)
            lobjParam(0).Direction = ParameterDirection.Output

            'The following will be used to capture any error that occurs in the back end.
            lobjParam(1) = New OracleParameter("p_out_error", OracleType.VarChar)
            lobjParam(1).Direction = ParameterDirection.Output
            'For some reason, not specifying the size here results in an error.
            lobjParam(1).Size = 255

            'Opening the connection.
            connection.Open()

            'Specifying the command (in this case, a stored procedure) to be executed and the connection
            'object to be used.
            command = New OracleCommand(CONST_FETCH_NAME_OF_EMPLOYEES, connection)
            command.CommandType = CommandType.StoredProcedure
            'Adding the parameters to be passed to the back end procedure.
            command.Parameters.AddRange(lobjParam)

            'Loading the values being returned from the back end into our datatable.
            dt.Load(command.ExecuteReader())

            'Checking if any error occured in the back end.
            If lobjParam(1).Value Is DBNull.Value Or lobjParam(1).Value.ToString().Equals("") Then
                'No error. Return the datatable.
                Return dt
            Else
                'An error occurred. Throw an exception with the error message returned from the back end.
                backend_error_msg = lobjParam(1).Value.ToString()
                Throw New Exception(backend_error_msg)
            End If

        Catch ex As Exception
            'Simply throw the exception that has been caught.
            'It will be handled in the code behind page.
            Throw ex
        Finally
            'The following code will ensure that the connection is closed after processing is done. Always.
            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Closed Then
                connection.Close()
            End If
            command.Parameters.Clear()
        End Try
    End Function

End Class
