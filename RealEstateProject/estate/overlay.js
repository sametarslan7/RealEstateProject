// Şu anki resim indeksi ve resim listesi
let currentImageIndex = 0;
let images = [];

// Fonksiyon: Seçilen listeleme detaylarını gösteren overlay açma
function openOverlay(imgElement) {
    // Tıklanan resmin bulunduğu satırın (tr) verilerini al
    const parent = imgElement.closest('tr');

    if (!parent) {
        console.error("Parent <tr> element not found.");
        return;
    }

    const title = parent.getAttribute('data-title');
    const imagesData = parent.getAttribute('data-images'); // Çoklu resimler
    const price = parent.getAttribute('data-price');
    const year = parent.getAttribute('data-year');
    const city = parent.getAttribute('data-city');
    const district = parent.getAttribute('data-district');
    const status = parent.getAttribute('data-status');
    const type = parent.getAttribute('data-type');
    const description = parent.getAttribute('data-description');

    try {
        images = JSON.parse(imagesData); // JSON formatını ayrıştır
    } catch (error) {
        console.error("Failed to parse images data:", error);
        images = [];
    }

    currentImageIndex = 0; // İlk resimle başla

    // Overlay'yi göster
    document.getElementById('overlay').style.display = 'block';

    // İlk resmi overlay'e ayarla
    if (images.length > 0) {
        document.getElementById('overlay-img').src = images[currentImageIndex];
    }

    // Overlay detaylarını ayarla
    document.getElementById('overlay-title').textContent = title || "No Title";
    document.getElementById('overlay-price').textContent = price || "N/A";
    document.getElementById('overlay-year').textContent = year || "N/A";
    document.getElementById('overlay-city').textContent = city || "N/A";
    document.getElementById('overlay-district').textContent = district || "N/A";
    document.getElementById('overlay-status').textContent = status || "N/A";
    document.getElementById('overlay-type').textContent = type || "N/A";
    document.getElementById('overlay-description').textContent = description || "No Description";
}


// Fonksiyon: Resim değiştirme (ileri/geri)
function changeImage(direction) {
    // Yeni resim indeksini hesapla
    currentImageIndex = (currentImageIndex + direction + images.length) % images.length;

    // Yeni resmi overlay'e ayarla
    document.getElementById('overlay-img').src = images[currentImageIndex];
}

// Fonksiyon: Overlay'yi kapama
function closeOverlay() {
    document.getElementById('overlay').style.display = 'none';
    images = []; // Resim listesini temizle
    currentImageIndex = 0;
}

// Overlay dışında bir yere tıklanırsa overlay'yi kapatma
document.getElementById('overlay').addEventListener('click', function (event) {
    // Sadece arka plana tıklanırsa kapat
    if (event.target === this) {
        closeOverlay();
    }
});

// Overlay içeriğine tıklanırsa overlay kapanmasını engelle
document.querySelector('.overlay-content').addEventListener('click', function (event) {
    event.stopPropagation();
});
