$(document).ready(function () {

    $("#btnSignIn").click(function () {
        if ($("#name").val() == "" || $("#password").val() == "") {
            alert("Enter Details");
        }
        else {
            if (validateName(document.getElementById("name").value)) {
                $.ajax({
                    url: '/api/values',
                    data: { id: $("#name").val(), password: $("#password").val() },
                    contentType: 'application/json; charset=UTF-8',
                    dataType: 'json',
                    type: 'GET',
                    success: function (result) {
                        if (result) {
                            jounalDisplay();
                            getData();
                            getJournals();
                            $.mobile.changePage("#Home");
                        }
                        else
                            alert("incorrect credentials");
                    },
                    error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
                });
            }
        }
    });

    var selectedjournal;

    $("#btnSubmit").click(function () {
        var flag = 0
        if ($("#txtJournal").val() != "") {
            if (validateName(document.getElementById("txtJournal").value)) {
                saveJournal($("#txtJournal").val())
            }
        }
        else {
            alert("Enter journal name")
        }
    });

    var saveJournal = function (jname) {
        $.ajax({
            url: '/api/values',
            data: { jname: jname },
            contentType: 'application/json; charset=UTF-8',
            dataType: 'json',
            type: 'GET',
            success: function (result) {
                alert("journal name accepted");
                jounalDisplay();
                getJournals();
                $.mobile.changePage("#Note");
            }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }

        });
    }

    var getJournals = function () {
        $.ajax({
            url: '/api/Journal',
            data: { UserId: 0 },
            contentType: 'application/json; charset=UTF-8',
            dataType: 'json',
            type: 'GET',
            success: function (result) {
                setJournals(result)
            }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
        });
    }

    var setJournals = function (journaldata) {
        $("#userjournals").empty();
        var userjournals = $("#userjournals");
        $.each(journaldata, function (i, journal) {
            userjournals.append($("<option></option>").val(journal["JournalId"]).html(journal["JournalName"]));
            $('select option:contains("' + $("#txtJournal").val() + '")').prop('selected', true);
        });
        userjournals.selectmenu('refresh', true);
    };


    $("#btnSave").click(function () {
        if ($("#PS_email").val() == "" || $("#PS_curPswd").val() == "" || $("#PS_newPswd").val == "" || $("#PS_rePswd").val() == "") {
            alert("Enter Details");
        }
        else {
            $.ajax({
                url: '/api/user',
                data: { password: $("#PS_curPswd").val() },
                contentType: 'application/json; charset=UTF-8',
                dataType: 'json',
                type: 'GET',
                success: function (result) {
                    authenticatePassword(result)
                }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }

            });
        }
    });

    var authenticatePassword = function (result) {
        if (result == false) {
            alert("enter correct old password");
            $.mobile.changePage("#ProfileSettings");
        }
        else {
            if ($("#PS_newPswd").val() == $("#PS_rePswd").val()) {
                $.ajax({
                    url: '/api/user',
                    data: { email: $("#PS_email").val(), password: $("#PS_newPswd").val(), mobile: $("#PS_mobile").val() },
                    contentType: 'application/json; charset=UTF-8',
                    dataType: 'json',
                    type: 'GET',
                    success: function (result) {
                        if (result == "Updated") {
                            alert("Settings Changed Sucessfully");
                            $.mobile.changePage("#Home");
                        }
                        else if (result == "") {
                            alert("Couldn't update");
                        }
                        else {
                            alert(result);
                        }
                    }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
                });
            }
            else {
                alert("Passwords didn't match");
                $.mobile.changePage("#ProfileSettings");
            }

        }
    }

    $("#btnRegister").click(function () {
        if ($("#txtName").val() == "" || $("#txtmail").val() == "" || $("#txtPassword").val() == "" || $("#txtMobile").val().toString() == "") {
            alert("Enter all fields")
        }
        else {
            if (validateName(document.getElementById("txtName").value)) {
                if ($("#txtPassword").val() == $("#txtRePassword").val()) {
                    $.ajax({
                        url: '/api/user',
                        data: { name: $("#txtName").val(), password: $("#txtPassword").val() },
                        contentType: 'application/json; charset=UTF-8',
                        dataType: 'json',
                        type: 'GET',
                        success: function (result) {
                            if (result == false) {
                                $.ajax({
                                    url: '/api/user',
                                    data: { name: $("#txtName").val(), email: $("#txtEmail").val(), password: $("#txtPassword").val(), mobileno: $("#txtMobile").val().toString() },
                                    contentType: 'application/json; charset=UTF-8',
                                    dataType: 'json',
                                    type: 'GET',
                                    success: function (result) {
                                        if (result == "Inserted") {
                                            alert("Registered Successfully");
                                            getData();
                                            $.mobile.changePage("#Home");
                                        }
                                        else {
                                            alert(result);
                                            $.mobile.changePage("#Registration");
                                        }
                                    }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
                                });
                            }

                            else {
                                alert("User already exists,Please sign in!!");
                                $.mobile.changePage("#Home");
                            }
                        }
                         , error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
                    });
                }
                else
                    alert("Passwords not similar")
            }
        }
    });
});


var getData = function () {
    $.ajax({
        url: '/api/user',
        contentType: 'application/json; charset=UTF-8',
        dataType: 'json',
        type: 'GET',
        success: function (result) {
            setData(result)
        }
         , error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
    });
}

var logOut = function () {
    $.ajax({
        url: '/api/values',
        data: { id: "", password: "", mobile: "" },
        contentType: 'application/json; charset=UTF-8',
        dataType: 'json',
        type: 'GET',
        success: function (result) {
            $.mobile.changePage("#MobileDiary");
            window.location.reload();
        }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
    });
}

var setData = function (userdata) {
    var properties = Object.keys(userdata);
    $.each(properties, function (i, property) {
        switch (property) {
            case "EmailId": $("#PS_email").attr('value', userdata[property]); break;
            case "MobileNo": $("#PS_mobile").attr('value', userdata[property]); break;
        }
    });
}

var jounalDisplay = function () {
    $.ajax({
        url: '/api/Journal',
        contentType: 'application/json; charset=UTF-8',
        dataType: 'json',
        type: 'GET',
        success: function (result) {
            setJounalData1(result)
        }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }

    });
}

var journalid = "";

var setJounalData1 = function (data) {
    $('#ulJournal').empty();
    $.each(data, function (i, journal) {
        var jdate = journal["JournalDate"].toString();
        $('#ulJournal').append('<li><a href="#" onclick="showEntries(\'' + journal["JournalId"] + '\');">' + journal["JournalName"] + '</a></li>');
    });
    $('#ulJournal').listview('refresh');
};

var showEntries = function (jid) {
    journalid = jid;
    $.ajax({
        url: '/api/Entries',
        data: { jourid: jid },
        contentType: 'application/json; charset=UTF-8',
        dataType: 'json',
        type: 'GET',
        success: function (result) {
            showData2(result)
        }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }

    });
}

var showData = function (data) {
    $('tbbody').empty();
    $.each(data, function (i, EntryTag) {
        var entries = Object.keys(EntryTag);
        var element = "";
        var id = "";
        var note = "";
        $.each(entries, function (j, productProp) {
            if (productProp == "EntryId")
                id = EntryTag[productProp];
            if (productProp == "Notes")
                note = EntryTag[productProp];
            if (productProp == "Time")
                element = element + '<tr><td><span>' + note + '</span></td><td><span>' + EntryTag[productProp] + '</span></td>';
        });
        element = element + '<td><span><a href="#" data-theme="c" data-role="button" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" class="ui-btn ui-shadow ui-btn-corner-all ui-btn-up-c" onclick="deleteEntry(\'' + id + '\');" >Delete</a></span></td></tr>';
        $('tbbody').append(element);
    });
    $.mobile.changePage("#entrypage");
}

var showData2 = function (data) {
    $('tbbody1').empty();
    var productProperties = Object.keys(data);
    $('#ulJournalView').empty();
    $.each(data, function (i, EntryTag) {
        var entries = Object.keys(EntryTag);
        var element = ""; var element1 = "";
        var id = "";
        var note = "";
        $.each(entries, function (j, productProp) {
            if (productProp == "EntryId")
                id = EntryTag[productProp];
            if (productProp == "Notes")
                note = EntryTag[productProp];
            if (productProp == "Time") {
                var p = note.split(".", 1);
                element1 = element1 + '<li><a href="#" onclick="showEntry(\'' + id + '\');"  ><p>' + p[0] + '...</p><p class="ui-li-aside"><strong>' + EntryTag[productProp] + '</strong></p></a>';
            }
        });
        element1 = element1 + '<a href="#" onclick="deleteEntry(\'' + id + '\');" class="delete">Delete</a></li>';
        $('#ulJournalView').append(element1);
    });
    $.mobile.changePage("#JournalDetailView");
    $('#ulJournalView').listview('refresh');
};

var showEntry = function (entryid) {
    $('tbbody1').empty();
    $.ajax({
        url: '/api/Entries',
        data: { id: 0, EntryId: entryid },
        contentType: 'application/json; charset=UTF-8',
        dataType: 'json',
        type: 'GET',
        success: function (result) {
            $('tbbody1').append('<p>' + result["Notes"] + '</p>');
        }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
    });
    $.mobile.changePage("#DetailsView");
};

var deleteEntry = function (entryid) {
    $.ajax({
        url: '/api/Entries',
        data: { id: entryid, dummy: "0" },
        contentType: 'application/json; charset=UTF-8',
        dataType: 'json',
        type: 'GET',
        success: function (result) {
            if (result)
                alert("deleted");
            showEntries(journalid);
            $('#ulJournalView').listview('refresh');
        }
         , error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
    });
}

var getEntries = function () {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) { dd = '0' + dd }
    if (mm < 10) { mm = '0' + mm }
    today = mm + '/' + dd + '/' + yyyy;
    var str = document.getElementById("date").value;
    var splitdate = str.split("-");
    if (parseInt(splitdate[0]) <= parseInt(yyyy)) {
        if (parseInt(splitdate[1]) <= parseInt(mm)) {
            if (parseInt(splitdate[2]) <= parseInt(dd)) {
                $.ajax({
                    url: '/api/Entries',
                    data: { year: splitdate[0], month: splitdate[1], day: splitdate[2], dummy: 0 },
                    contentType: 'application/json; charset=UTF-8',
                    dataType: 'json',
                    type: 'GET',
                    success: function (result) {
                        setEntries(result);
                    }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
                });
            }
            else
                alert("select proper date");
        }
        else
            alert("select proper date");
    }
    else
        alert("select proper date");
}

var setEntries = function (data) {
    $('#ulEntryView li').remove();
    $("#ulEntryView").html('');
    var newentry = "";
    $.each(data, function (i, entry) {
        newentry = '<li><p>' + entry["Notes"] + '</p></li>';
        $('#ulEntryView').append(newentry);
    });
    if (newentry == "")
        $('#ulEntryView').append("No Entries");

}

function validateName(username) {
    var iChars = "!@#$%^&*()+=-[]\\\';,./{}|\":<>?";
    for (var i = 0; i < username.length; i++) {
        if (iChars.indexOf(username.charAt(i)) != -1) {
            alert("Special Characters are not allowed");
            return false;
        }
    }
    return true;
}

var checkJournal = function () {
    $.ajax({
        url: '/api/Values',
        contentType: 'application/json; charset=UTF-8',
        dataType: 'json',
        type: 'GET',
        success: function (result) {
            if (result != "NotFound") {
                alert("Journal for the day already created");
                $.mobile.changePage("#Note");
            }
            else {
                $.mobile.changePage("#Journal");
            }
        }, error: function (data, textStatus, jqXHR) { $.mobile.changePage("#ErrorPage"); }
    });
}