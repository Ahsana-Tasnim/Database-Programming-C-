function deleteEmployee(empId) {
    $.ajax({
        type: 'DELETE',
        url: '/Home/DeleteEmployee/?empId=' + empId,
    data: { },
    success: function (data) {
        alert('Employee ID: ' + empId + ' was deleted.');
    window.location.reload();
            },
    contentType: "application/json; charset=utf-8",
    dataType: "json"
        });
    }

function updateEmployee(rowId) {
    var fName = $('#' + rowId).find('[name="fName"]').val();
    var lName = $('#' + rowId).find('[name="lName"]').val();
    var empId = rowId.replace('tr_', ''); // $('#' + rowId).find('[name="empId"]').val();

    var employee = {
    First_Name: fName,
    Last_Name: lName,
    Emp_Id: empId
        };

    $.ajax({
        type: 'POST',
        url: '/Home/UpsertEmployee/',
    data: JSON.stringify(employee),
    success: function (data) {
        alert('Employee: ' + fName + ' was updated.');
    window.location.reload();
            },
    contentType: "application/json; charset=utf-8",
    dataType: "json"
        });
    }


    function saveEmployee() {
        var fName = document.getElementById('fName').value;
    var lName = document.getElementById('lName').value;
    var newEmpDepoId = document.getElementById('depoItem').value;

    var employee = {
        First_Name: fName,
    Last_Name: lName,
    Dept_Id: newEmpDepoId
        };

    $.ajax({
        type: 'POST',
    url: '/Home/UpsertEmployee/',
    data: JSON.stringify(employee),
    success: function (data) {
        alert('Employee: ' + fName + ' was saved.');
    window.location.reload();
            },
    contentType: "application/json; charset=utf-8",
    dataType: "json"
        });
    }

    function searchFName() {
        //txtSearch input value
        var searchTermFName = document.getElementById('txtSearchFName').value;
        window.location.href = "/Home/Index/?searchTermFName=" + encodeURIComponent(searchTermFName);
    }

    function searchLName() {
        var searchTermLName = document.getElementById('txtSearchLName').value;
        window.location.href = "/Home/Index/?searchTermLName=" + encodeURIComponent(searchTermLName);
    }