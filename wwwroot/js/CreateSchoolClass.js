const form = document.getElementById("createClassform");
const submit = document.getElementById("createClassButton");
let classLetter = document.getElementById("ClassLetter");
let yearOfStudy = document.getElementById("YearOfStudy");

//showing an alert message when the form is submitted
submit.addEventListener("click", () => {
    let yearValue = yearOfStudy.options[yearOfStudy.selectedIndex].text;
    let classLetterValue = classLetter.options[classLetter.selectedIndex].text;

    setTimeout(() => {
        form.submit();
    }, 2000);

    swal({
        title: "Good job!",
        text: "Success! The class " + yearValue + classLetterValue + " has been created!",
        icon: "success",
    });
});

//changing the class letter depending on user input for school year
yearOfStudy.addEventListener('change', changeLetter);
function changeLetter() {
    let yearValue = yearOfStudy.value;
    classLetter.value = yearValue;
}