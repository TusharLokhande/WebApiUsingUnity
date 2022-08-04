$(document).ready(() => {
    DropDown('dropdown', '#DepartmentId');
    DropDown('reportingmanager', '#ReportingManagerId');

    $("#submit").click((evt) => {
        evt.preventDefault();
        let EName = $("#EName").val();
        let Email = $("#Email").val();
        let DateOfBirth = $("#DateOfBirth").val();
        let DepartmentId = $("#DepartmentId").val();
        let ReportingManagerId = $("#ReportingManagerId").val();

        console.log({ EName, Email, DateOfBirth, DepartmentId, ReportingManagerId });
        let obj = { EName, Email, DateOfBirth, DepartmentId, ReportingManagerId };
        $.ajax({
            type: "POST",
            url: "http://localhost:50866/api/employee/Post",
            data: obj ,
            success: function (data) {
                console.log(data);
                alert("Save Complete");
            }
        });
    });
})

function DropDown(name, id) {
    $.ajax({
        type: 'GET',
        dataType: "json",
        url: `http://localhost:50866/api/employee/${name}`,
        success: function (data, status, xhr) {
            let i = '';
            data.map(data => { i = i + `<option value=${data.Id}>${data.Name}</option>`})
            console.log("done");
            $(id).html(i);
        }
    });
}

function Tp() {
    let EName = $("#EName").val();
    let Email = $("#Email").val();
    let DateOfBirth = $("#DateOfBirth").val();
    let DepartmentId = $("#DepartmentId").val();
    let ReportingManagerId = $("#ReportingManagerId").val

    console.log({ EName, Email, DateOfBirth, DepartmentId, ReportingManagerId });
}