const form = document.getElementById("createSubjectform");
const submit = document.getElementById("createSubjectButton");
let subjectName = document.getElementById("Name");
let hoursPerWeek = document.getElementById("HoursPerWeek");

//showing an alert message when the form is submitted
submit.addEventListener("click", () => {
    let subjectNameValue = subjectName.value;
    let hoursPerWeekValue = hoursPerWeek.value;

    if (checkName(subjectNameValue) && checkHoursPerWeek(hoursPerWeekValue)) {
        setTimeout(() => {
            form.submit();
        }, 1700);

        Swal.fire({
            title: "Good job!",
            text: "Success! Subject '" + subjectNameValue + "' has been added!",
            icon: "success",
        });
    }
    else {
        form.submit();
    }
});

function checkName(name) {
    if (name == "") {
        return false;
    }
    else if (name.length <= 1) {
        return false;
    }
    else if (!(/^[a-zA-Z]+$/.test(name))) {
        return false;
    }
    else {
        return true;
    }
}

function checkHoursPerWeek(hours) {
    if (typeof (hours) != "number") {
        return false;
    }
    else {
        return true;
    }
}