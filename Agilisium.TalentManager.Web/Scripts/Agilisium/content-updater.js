
formatJsonDateString = function (value) {
    var a;
    if (typeof value == 'string') {
        a = /\/Date\((\d*)\)\//.exec(value);
        if (a) {
            var dateVal = new Date(+a[1]);
            var month = dateVal.getMonth() + 1;
            var day = dateVal.getDate();
            var year = dateVal.getFullYear();
            return month + "/" + day + "/" + year;
        }
    }
    return value;
}

getProjectManagerByProjectID = function () {
    $("#projectManagerLabel").empty();
    if ($("#ProjectID").val().length > 0) {
        $.ajax({
            url: rootUrl + "Project/GetProjectManagerName",
            type: "POST",
            data: { projectID: $("#ProjectID").val() },
            success: function (data) {
                $("#projectManagerLabel").text(data);
            },
            error: function (xhr, status) {
                alert("Error has occured while getting the project manager name");
            }
        });
    }
}

function getEmployeePercentageOfAllocation() {
    if ($("#EmployeeID").val().length > 0) {
        var v_data = {};
        v_data.empID = $("#EmployeeID").val();
        v_data.prjID = $("#ProjectID").val();

        $.ajax({
            url: rootUrl + "Allocation/GetPercentageOfAllocation",
            type: "POST",
            data: JSON.stringify(v_data),
            contentType: "application/json",
            success: function (data) {
                $("#howMuchOccupied").text(data);
            },
            error: function () {
                alert("Error has occured while loading employee details");
            }
        });
    }
}

function getEmployeeOtherProjectAllocations() {
    if ($("#EmployeeID").val().length > 0) {
        $.ajax({
            url: rootUrl + "Allocation/EmployeeAllocations",
            cache: false,
            type: "GET",
            contentType: "application/html; charset=utf-8",
            data: { empID: $("#EmployeeID").val() },
            success: function (data) {
                $("#employeeAllocationDiv").html(data);
            },
            error: function (xhr, status) {
                alert("Error has occured while loading employee allocation details");
            }
        });
    }
}

function loadEmployeeDetailsForAllocationEditPage() {
    if ($("#EmployeeID").val().length > 0) {
        $.ajax({
            url: rootUrl + "Employee/GetEmployeeDetails",
            type: "POST",
            data: { id: $("#EmployeeID").val() },
            success: function (data) {
                $("#employeeID").text(data["EmployeeID"]);
                $("#employeeType").text(data["EmploymentTypeName"]);
                $("#primarySkills").text(data["PrimarySkills"]);
                $("#secondarySkills").text(data["SecondarySkills"]);
            },
            error: function () {
                alert("Error has occured while loading project details");
            }
        });
    }
}

function updateProjectDetailsSectionOnAllocationPage() {
    if ($("#ProjectID").val().length > 0) {
        $.ajax({
            url: rootUrl + "Allocation/GetProjectDetails",
            type: "POST",
            data: { projectID: $("#ProjectID").val() },
            success: function (data) {
                var sDate = formatJsonDateString(data["StartDate"]);
                var eDate = formatJsonDateString(data["EndDate"]);

                // updating project details section
                $("#pmName").text(data["ProjectManagerName"]);
                $("#projectStartDate").text(sDate);
                $("#projectEndDate").text(eDate);
                $("#projectType").text(data["ProjectTypeName"]);
            },
            error: function () {
                alert("Error has occured while loading project details");
            }
        });
    }
}

function getPracticeManagerNameForSubPracticePage() {
    if ($("#PracticeID").val().length > 0) {
        $.ajax({
            url: rootUrl + "SubPractice/GetPracticeName",
            type: "POST",
            data: { id: $("#PracticeID").val() },
            success: function (data) {
                if (data.length == 0) {
                    $("#practiceManagerName").text("  Practice Manager: None");
                }
                else {
                    $("#practiceManagerName").text("  Practice Manager: " + data);
                }
            },
            error: function () {
                alert("Unable to retrieve the Pratice Manager Name");
            }
        });
    }
}

function generateEmployeeIDForEmpPage() {
    if ($("#EmploymentTypeID").val().length > 0) {
        $.ajax({
            url: rootUrl + "Employee/GenerateNewEmployeeID",
            type: "POST",
            data: { id: $("#EmploymentTypeID").val() },
            success: function (data) {
                $("#EmployeeID").val(data);
                $("#empID").val(data);
            },
            error: function (eid) { alert("Error while generating new Employee ID"); }
        });
    }
}

function loadSubPracticeDropDownForEmpPage() {
    if ($("#PracticeID").val().length > 0) {
        $.ajax({
            url: rootUrl + "Employee/GetSubPracticeList",
            type: 'POST',
            data: { id: $("#PracticeID").val() },
            success: function (data) {
                $('#SubPracticeID').empty();
                $("#SubPracticeID").append($("<option></option>").val(0).text("Please Select"));
                $.each(data, function () {
                    $("#SubPracticeID").append($("<option></option>").val(parseInt(this['Value'])).text(this['Text']));
                });
            },
            error: function (xhr) { alert('Error while loading the Sub Practice list'); }
        });
    }
}

function getPracticeManagerNameForEmployeePage() {
    $("#txtPracticeManager").text("");
    $("#txtSubPracticeManager").text("");

    if ($("#PracticeID").val().length > 0) {
        $.ajax({
            url: rootUrl + "SubPractice/GetPracticeName",
            type: "POST",
            data: { id: $("#PracticeID").val() },
            success: function (data) {
                if (data.length == 0) {
                    $("#txtPracticeManager").text("Not assigned yet");
                }
                else {
                    $("#txtPracticeManager").text(data);
                }
            },
            error: function () {
                alert("Unable to retrieve the Pratice Manager Name");
            }
        });
    }
}

function getSubPracticeManagerNameForEmployeePage() {
    if ($("#SubPracticeID").val().length > 0) {

        $("#txtSubPracticeManager").text("");

        $.ajax({
            url: rootUrl + "SubPractice/GetSubPracticeManagerName",
            type: "POST",
            data: { id: $("#SubPracticeID").val() },
            success: function (data) {
                if (data.length == 0) {
                    $("#txtSubPracticeManager").text("Not assigned yet");
                }
                else {
                    $("#txtSubPracticeManager").text(data);
                }
            },
            error: function () {
                alert("Unable to retrieve the Pratice Manager Name");
            }
        });
    }
}

function loadPracticeDropDownListForEmpPage() {
    if ($("#BusinessUnitID").val().length == 0) return;
    $("#PracticeID").empty();
    $("#PracticeID").append($("<option></option>").val(0).text("Please Select"));
    $("#SubPracticeID").empty();
    $("#SubPracticeID").append($("<option></option>").val(0).text("Please Select"));

    $.ajax({
        url: rootUrl + "Practice/GetPracticesByBuID",
        type: "POST",
        data: { buID: $("#BusinessUnitID").val() },
        success: function (data) {

            $.each(data, function () {
                $("#PracticeID").append($("<option></option>").val(parseInt(this['Value'])).text(this['Text']));
            });
        },
        error: function () {
            alert("Error has occured while loading the Practices for the selected BU");
        }
    });
}

function loadPracticeDropDownListForProjectPage() {
    if ($("#BusinessUnitID").val().length == 0) return;
    $("#PracticeID").empty();
    $("#PracticeID").append($("<option></option>").val(0).text("Please Select"));

    $.ajax({
        url: rootUrl + "Practice/GetPracticesByBuID",
        type: "POST",
        data: { buID: $("#BusinessUnitID").val() },
        success: function (data) {

            $.each(data, function () {
                $("#PracticeID").append($("<option></option>").val(parseInt(this['Value'])).text(this['Text']));
            });
        },
        error: function () {
            alert("Error has occured while loading the Practices for the selected Business Unit");
        }
    });
}

//$(document).ajaxStart(function () {
//    $("#loading").show();
//});

//$(document).ajaxComplete(function () {
//    $("#loading").hide();
//});

