// Define a TypeScript class to manage your UI and interactions
class MyApp {
    constructor() {
        // Add an event listener to the button
        const button = document.getElementById("goToMainPage");
        if (button) {
            button.addEventListener("click", this.goToMainPage.bind(this));
        }
    }

    // Function to handle button click
    goToMainPage() {
        // Perform the desired action here, e.g., navigation
        alert("Navigating to the main page");
    }
}

// Initialize your application when the DOM is ready
document.addEventListener("DOMContentLoaded", () => {
    const app = new MyApp();
});
