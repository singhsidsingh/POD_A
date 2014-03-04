Public Class CONSTANTS
    'The nomenclature: <schema name>.<package name>.<procedure name>

    'Procedures for adding new stuff to the back end'
    '-----------------------------------------------'
    Public Const CONST_ADD_NEW_ROLE As String = "sidsinghdba.pk_pod_a.pr_add_new_role"
    Public Const CONST_ADD_NEW_PROJECT As String = "sidsinghdba.pk_pod_a.pr_add_new_project"
    Public Const CONST_ADD_NEW_MODULE As String = "sidsinghdba.pk_pod_a.pr_add_new_module"
    Public Const CONST_ADD_NEW_EMPLOYEE As String = "sidsinghdba.pk_pod_a.pr_add_new_employee"
    Public Const CONST_ADD_NEW_WEEKLYREPORT As String = "sidsinghdba.pk_pod_a.pr_add_new_weeklyreport"
    '-----------------------------------------------'

    'Procedures for retrieving stuff from the back end'
    '-----------------------------------------------'
    'The following fetch complete data from tables, no filters applied.
    Public Const CONST_FETCH_ALL_ROLES As String = "sidsinghdba.pk_pod_a.pr_fetch_all_roles"
    Public Const CONST_FETCH_ALL_PROJECTS As String = "sidsinghdba.pk_pod_a.pr_fetch_all_projects"
    Public Const CONST_FETCH_ALL_MODULES As String = "sidsinghdba.pk_pod_a.pr_fetch_all_modules"
    Public Const CONST_FETCH_ALL_EMPLOYEES As String = "sidsinghdba.pk_pod_a.pr_fetch_all_employees"
    Public Const CONST_FETCH_ALL_WEEKLYREPORTS As String = "sidsinghdba.pk_pod_a.pr_fetch_all_weeklyreports"

    'The following fetch certain columns but all rows from tables.
    Public Const CONST_FETCH_ID_NAME_DESC_OF_PROJECTS As String = "sidsinghdba.pk_pod_a.pr_fetch_id_name_desc_of_projects"
    Public Const CONST_FETCH_SSO_OF_EMPLOYEES As String = "sidsinghdba.pk_pod_a.pr_fetch_sso_of_employees"
    Public Const CONST_FETCH_EMPID_OF_EMPLOYEES As String = "sidsinghdba.pk_pod_a.pr_fetch_empid_of_employees"
    Public Const CONST_FETCH_NAME_OF_EMPLOYEES As String = "sidsinghdba.pk_pod_a.pr_fetch_name_of_employees"

    'The following fetch certain/all columns and certain rows from tables.
    Public Const CONST_FETCH_ALL_MODULES_OF_PROJECT As String = "sidsinghdba.pk_pod_a.pr_fetch_all_modules_of_projectid"
    Public Const CONST_FETCH_ALL_DETAILS_OF_EMPLOYEE_FROM_SSO As String = "sidsinghdba.pk_pod_a.pr_fetch_all_details_from_sso"
    Public Const CONST_FETCH_ALL_DETAILS_OF_EMPLOYEE_FROM_EMPID As String = "sidsinghdba.pk_pod_a.pr_fetch_all_details_from_empid"
    Public Const CONST_FETCH_ALL_DETAILS_OF_EMPLOYEE_FROM_NAME As String = "sidsinghdba.pk_pod_a.pr_fetch_all_details_from_name"
    '-----------------------------------------------'


End Class
