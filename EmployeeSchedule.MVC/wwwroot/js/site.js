// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#employeeSearch').on('click', function (e) {
    var text = $('#employeeSearchText').val();
    Search(text);
});

$('#scheduleSearch').on('click', function (e) {
    var text = $('#scheduleSearchText').val();
    var employeeId = $('#scheduleEmployeeSearch').val();
    var date = $('#scheduleSearchDate').val();
    SearchSchedule(text, employeeId,date);
});

$('#employeeSort').on('click', function (e) {
    var text = $('#employeeSortCriteria').val();
    Sort(text);
});
$('#employeeSummarySearch').on('click', function (e) {
    var text = $('#employeeSearchSummaryText').val();
    SearchSummary(text);
});

function Search(text) {
    $.ajax({
        type: 'GET',
        url: '/Employee/Search',
        data: { "criteria": text },
        cache: false,
        dataType: "html"
        //success: function (result) {

        //    $('#employeeResultInsert').html(result);
        //}
    })
        .done(function (result) {

            $('#employeeResultInsert').html(result);
        })
        .fail(function (xhr) {
            console.log('error : ' + xhr.status + ' - ' + xhr.statusText + ' - ' + xhr.responseText);
        })
}

function SearchSchedule(text,employeeId,date) {
    $.ajax({
        type: 'GET',
        url: '/Schedule/Search',
        data: { "text": text, "employeeId": employeeId, "date": date },
        cache: false,
        dataType: "html"
        //success: function (result) {

        //    $('#employeeResultInsert').html(result);
        //}
    })
        .done(function (result) {

            $('#scheduleResultInsert').html(result);
        })
        .fail(function (xhr) {
            console.log('error : ' + xhr.status + ' - ' + xhr.statusText + ' - ' + xhr.responseText);
        })
}

$('#employeeSearch').on('click', function (e) {
    var text = $('#employeeSearchText').val();
    Search(text);
});

function Sort(criteria) {
    $.ajax({
        type: 'GET',
        url: '/Employee/Sort',
        data: { "criteria": criteria },
        cache: false,
        dataType: "html"
        //success: function (result) {

        //    $('#employeeResultInsert').html(result);
        //}
    })
        .done(function (result) {

            $('#employeeResultInsert').html(result);
        })
        .fail(function (xhr) {
            console.log('error : ' + xhr.status + ' - ' + xhr.statusText + ' - ' + xhr.responseText);
        })
}
function SearchSummary(text) {
    
    $.ajax({
        type: 'GET',
        url: '/Employee/EmployeeSummarySearch',
        data: { "criteria": text },
        cache: false,
        dataType: "html"
        //success: function (result) {

        //    $('#employeeResultInsert').html(result);
        //}
    })
        .done(function (result) {

            $('#employeeSummaryResultInsert').html(result);
        })
        .fail(function (xhr) {
            console.log('error : ' + xhr.status + ' - ' + xhr.statusText + ' - ' + xhr.responseText);
        })
}
