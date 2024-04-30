
//dropdown settings for navbar (User tab)
let dropList = document.querySelector(".dropdown-settings");
let title = document.querySelector(".userNavbar");
let coursesLink = document.querySelector(".link-courses");

title.addEventListener("click", function () {
    if (window.innerWidth < 769) {
        dropList.classList.toggle("dropdown-active");
        coursesLink.classList.toggle("courses-active");

        title.style.display = "block";
        title.style.textAlign = "center";
        title.style.color = "#ff9774";
    }

    if (dropList.classList.contains("dropdown-active") == false) {
        title.style.color = "#fff";

    }
});

//navbar styling for phone - click on burger icon
const navSlide = () => {
    const burger = document.querySelector(".burger");
    const nav = document.querySelector(".nav-links");
    const navLinks = document.querySelectorAll(".link");


    burger.addEventListener("click", () => {
        //toggle nav
        nav.classList.toggle("nav-active");
        if (dropList.class = "dropdown-active") {
            dropList.classList.remove("dropdown-active");
        }

        //animate links
        navLinks.forEach((link, index) => {
            if (link.style.animation) {
                link.style.animation = ''
            } else {
                link.style.animation = `navLinkFade 0.5s ease forwards ${index / 7 + 0.2}s`;
            }
        });

        //burger animation
        burger.classList.toggle("toggle");
    });
}

navSlide();