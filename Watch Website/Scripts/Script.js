
function convertImageToBase64(event) {
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.onload = function () {
        const base64String = reader.result.split(',')[1]; // Extract the Base64 string
        document.getElementById('displayImage').src = 'data:image/jpeg;base64,' + base64String; // Display the image
    }

    if (file) {
        reader.readAsDataURL(file);
    }
}