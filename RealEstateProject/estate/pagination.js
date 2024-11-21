// Pagination Variables
let currentPage = 1; // Başlangıç sayfası
const itemsPerPage = 7; // Her sayfada gösterilecek maksimum ilan sayısı
const listings = document.querySelectorAll(".listing-item"); // Tüm ilanları seç
const totalPages = Math.ceil(listings.length / itemsPerPage); // Toplam sayfa sayısı

// Function to Render Listings for the Current Page
function renderListings() {
    // Tüm ilanları gizle
    listings.forEach((item, index) => {
        item.style.display = "none";
    });

    // Mevcut sayfada gösterilecek ilanları belirle
    const startIndex = (currentPage - 1) * itemsPerPage;
    const endIndex = startIndex + itemsPerPage;

    listings.forEach((item, index) => {
        if (index >= startIndex && index < endIndex) {
            item.style.display = "table-row"; // İlanları tablo satırı olarak göster
        }
    });

    // Sayfa numaralarını güncelle
    updatePageButtons();
}

// Function to Navigate to the Previous Page
function previousListing() {
    if (currentPage > 1) {
        currentPage--;
        renderListings();
    }
}

// Function to Navigate to the Next Page
function nextListing() {
    if (currentPage < totalPages) {
        currentPage++;
        renderListings();
    }
}

// Function to Update Page Buttons Visibility
function updatePageButtons() {
    const leftArrow = document.querySelector(".left-arrow");
    const rightArrow = document.querySelector(".right-arrow");

    // Sol ok düğmesini göster/gizle
    if (currentPage === 1) {
        leftArrow.style.display = "none";
    } else {
        leftArrow.style.display = "block";
    }

    // Sağ ok düğmesini göster/gizle
    if (currentPage === totalPages) {
        rightArrow.style.display = "none";
    } else {
        rightArrow.style.display = "block";
    }
}

// Initialize Pagination
renderListings();
