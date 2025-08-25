document.addEventListener('DOMContentLoaded', function () {
    'use strict';

    const tripModal = document.getElementById('trip-modal');
    const modalContent = document.getElementById('modal-content');
    const navbar = document.getElementById('navbar');
    const mobileMenuButton = document.getElementById('mobile-menu-button');
    const mobileMenu = document.getElementById('mobile-menu');
    const closeMobileMenuButton = document.getElementById('close-mobile-menu');
    const navLinks = document.querySelectorAll('.nav-link');
    const scrollToTopBtn = document.getElementById("scrollToTopBtn");
    const langFlags = document.querySelectorAll('.lang-flag');
    const contactForm = document.getElementById("contactForm");
    const testimonialForm = document.getElementById('testimonialForm');
    const notification = document.getElementById('notification');
    const notificationMessage = document.getElementById('notification-message');
    const currentYearSpan = document.getElementById('currentYear');
    const galleryContainer = document.querySelector('.gallery-container');
    const whatsappFab = document.getElementById('whatsapp-fab');

    const supportedLanguages = ['en', 'ar', 'de', 'ro'];
    let currentLang = localStorage.getItem('preferredLang') || 'en';

    const messages = {
        fillFields: { en: 'Please fill all fields.', ar: 'يرجى ملء جميع الحقول.', de: 'Bitte füllen Sie alle Felder aus.', ro: 'Vă rugăm să completați toate câmpurile.' },
        redirecting: { en: 'Redirecting to WhatsApp...', ar: 'جارٍ التحويل إلى واتساب...', de: 'Weiterleitung zu WhatsApp...', ro: 'Redirecționare către WhatsApp...' },
        invalidEmail: { en: 'Please enter a valid email.', ar: 'يرجى إدخال بريد إلكتروني صحيح.', de: 'Bitte geben Sie eine gültige E-Mail-Adresse ein.', ro: 'Vă rugăm să introduceți un email valid.' },
        reviewSubmitted: { en: 'Thank you for your review!', ar: 'شكراً لك على رأيك!', de: 'Vielen Dank für Ihre Bewertung!', ro: 'Mulțumim pentru recenzia ta!' },
        fillReviewFields: { en: 'Please fill all review fields.', ar: 'يرجى ملء جميع حقول الرأي.', de: 'Bitte füllen Sie alle Bewertungsfelder aus.', ro: 'Vă rugăm să completați toate câmpurile recenziei.' }
    };

    AOS.init({
        duration: 800,
        once: true,
        offset: 50,
    });

    if (navbar) {
        window.addEventListener('scroll', () => {
            if (window.scrollY > 50) {
                navbar.classList.add('bg-slate-800/80', 'backdrop-blur-sm', 'shadow-lg');
            } else {
                navbar.classList.remove('bg-slate-800/80', 'backdrop-blur-sm', 'shadow-lg');
            }
        });
    }
    if (mobileMenuButton && mobileMenu && closeMobileMenuButton) {
        const menuIcon = mobileMenuButton.querySelector('i');

        const openMenu = () => {
            mobileMenu.classList.add('is-open');
            menuIcon.classList.replace('fa-bars', 'fa-times');
            document.body.style.overflow = 'hidden';
        };

        const closeMenu = () => {
            mobileMenu.classList.remove('is-open');
            menuIcon.classList.replace('fa-times', 'fa-bars');
            document.body.style.overflow = '';
        };

        const toggleMenu = () => {
            const isOpen = mobileMenu.classList.contains('is-open');
            if (isOpen) {
                closeMenu();
            } else {
                openMenu();
            }
        };

        mobileMenuButton.addEventListener('click', toggleMenu);

        closeMobileMenuButton.addEventListener('click', closeMenu);

        const mobileNavLinks = mobileMenu.querySelectorAll('a');
        mobileNavLinks.forEach(link => {
            link.addEventListener('click', closeMenu);
        });

    } else {
        console.error('Mobile menu elements not found. Please check the IDs in your HTML file.');
    }

    function updateLanguage(lang) {
        if (!supportedLanguages.includes(lang)) return;

        currentLang = lang;
        localStorage.setItem('preferredLang', lang);
        document.documentElement.lang = lang;
        document.documentElement.dir = lang === 'ar' ? 'rtl' : 'ltr';

        document.querySelectorAll('[data-en]').forEach(el => {
            const translation = el.getAttribute(`data-${lang}`);
            if (translation) el.innerHTML = translation;
        });

        document.querySelectorAll('[data-placeholder-en]').forEach(el => {
            const translation = el.getAttribute(`data-${lang}-placeholder`);
            if (translation) el.placeholder = translation;
        });

        document.querySelectorAll('.trip-card').forEach(card => {
            const nameEl = card.querySelector('.trip-card-name');
            if (nameEl) nameEl.innerText = nameEl.dataset[lang] || nameEl.dataset['en'];
        });

        if (lang === 'ar') document.body.classList.add('font-cairo');
        else document.body.classList.remove('font-cairo');

        langFlags.forEach(flag => {
            flag.classList.toggle('active', flag.dataset.lang === lang);
        });
    }

    langFlags.forEach(flag => {
        flag.addEventListener('click', () => updateLanguage(flag.dataset.lang));
    });

    function openTripModal(tripId) {
        if (!tripId) return;

        modalContent.innerHTML = `<div class="w-full h-96 flex items-center justify-center"><i class="fas fa-spinner fa-spin text-4xl text-primary-dark"></i></div>`;
        tripModal.classList.remove('hidden');
        tripModal.classList.add('flex');
        setTimeout(() => {
            tripModal.classList.remove('opacity-0');
            modalContent.classList.remove('scale-95', 'opacity-0');
        }, 10);

        fetch(`/Home/GetTripDetails?id=${tripId}`)
            .then(response => {
                if (!response.ok) throw new Error('Network response was not ok');
                return response.json();
            })
            .then(data => {
                const langSuffix = currentLang.charAt(0).toUpperCase() + currentLang.slice(1);
                const name = data[`name${langSuffix}`];
                const description = data[`description${langSuffix}`];
                const imageUrl = data.imageUrl || 'https://placehold.co/600x800/00acc1/ffffff?text=Arzu+Trip';

                modalContent.innerHTML = `
                    <img src="${imageUrl}" alt="${name}" class="modal-image">
                    <div class="modal-content-wrapper relative">
                        <button id="close-modal" class="close-button">&times;</button>
                        <h2 class="modal-title">${name}</h2>
                        <p class="modal-price">$${data.price.toFixed(2)}</p>
                        <p class="modal-description">${description}</p>
                        <a href="#contact" class="modal-book-btn book-now-btn">
                            <span data-en="Book Now" data-ar="احجز الآن" data-de="Jetzt buchen" data-ro="Rezervă acum">Book Now</span>
                        </a>
                    </div>`;

                updateLanguage(currentLang);
                document.getElementById('close-modal').addEventListener('click', closeTripModal);
                document.querySelector('.book-now-btn').addEventListener('click', () => closeTripModal());
            })
            .catch(error => {
                console.error('Error fetching trip details:', error);
                modalContent.innerHTML = `<div class="w-full p-8 text-center"><p class="text-red-500">Sorry, we couldn't load the trip details.</p><button id="close-modal" class="btn-wave mt-4">Close</button></div>`;
                document.getElementById('close-modal').addEventListener('click', closeTripModal);
            });
    }

    function closeTripModal() {
        modalContent.classList.add('scale-95', 'opacity-0');
        tripModal.classList.add('opacity-0');
        setTimeout(() => {
            tripModal.classList.add('hidden');
            tripModal.classList.remove('flex');
        }, 300);
    }

    document.querySelectorAll('.details-btn').forEach(button => {
        button.addEventListener('click', function (e) {
            e.preventDefault();
            const tripId = this.closest('.trip-card')?.dataset.tripId;
            openTripModal(tripId);
        });
    });

    tripModal.addEventListener('click', (e) => {
        if (e.target === tripModal) closeTripModal();
    });

    document.addEventListener('keydown', (e) => {
        if (e.key === "Escape" && !tripModal.classList.contains('hidden')) closeTripModal();
    });

    window.addEventListener('scroll', () => {
        if (window.scrollY > 50) navbar.classList.add('nav-scrolled');
        else navbar.classList.remove('nav-scrolled');

        let currentSection = '';
        document.querySelectorAll('section[id]').forEach(section => {
            const sectionTop = section.offsetTop;
            if (pageYOffset >= sectionTop - 90) currentSection = section.getAttribute('id');
        });

        navLinks.forEach(link => {
            link.classList.remove('active');
            if (link.getAttribute('href').substring(1) === currentSection) link.classList.add('active');
        });

        if (window.scrollY > 300) scrollToTopBtn.classList.remove("hidden");
        else scrollToTopBtn.classList.add("hidden");
    });

    scrollToTopBtn.addEventListener("click", () => window.scrollTo({ top: 0, behavior: "smooth" }));

    function showNotification(message, type = 'success') {
        if (!notification || !notificationMessage) return;
        notificationMessage.textContent = message;
        notification.className = 'fixed bottom-5 left-1/2 -translate-x-1/2 text-white py-2 px-6 rounded-full shadow-lg z-50 transition-all duration-300 transform opacity-0 -translate-y-4';
        setTimeout(() => {
            notification.classList.remove('hidden');
            notification.classList.add(type === 'error' ? 'bg-red-500' : 'bg-green-500');
            notification.classList.remove('opacity-0', '-translate-y-4');
        }, 100);
        setTimeout(() => {
            notification.classList.add('opacity-0', '-translate-y-4');
            setTimeout(() => notification.classList.add('hidden'), 300);
        }, 3000);
    }

    function validateEmail(email) {
        const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(String(email).toLowerCase());
    }

    if (contactForm) {
        contactForm.addEventListener("submit", function (e) {
            e.preventDefault();
            const name = document.getElementById("UserName").value.trim();
            const email = document.getElementById("UserEmail").value.trim();
            const message = document.getElementById("message").value.trim();
            if (!name || !email || !message) {
                showNotification(messages.fillFields[currentLang], 'error');
                return;
            }
            if (!validateEmail(email)) {
                showNotification(messages.invalidEmail[currentLang], 'error');
                return;
            }
            const whatsappNumber = "201234567890";
            const whatsappMessage = `Name: ${name}%0AEmail: ${email}%0AMessage: ${message}`;
            const whatsappURL = `https://wa.me/${whatsappNumber}?text=${whatsappMessage}`;
            showNotification(messages.redirecting[currentLang], 'success');
            setTimeout(() => {
                window.open(whatsappURL, "_blank");
                contactForm.reset();
            }, 1500);
        });
    }

    if (testimonialForm) {
        testimonialForm.addEventListener('submit', function (e) {
            const name = document.getElementById('testimonialName').value.trim();
            const country = document.getElementById('testimonialCountry').value.trim();
            const message = document.getElementById('testimonialMessage').value.trim();
            if (!name || !country || !message) {
                e.preventDefault();
                showNotification(messages.fillReviewFields[currentLang], 'error');
                return;
            }
            showNotification(messages.reviewSubmitted[currentLang], 'success');
        });
    }

    if (galleryContainer) {
        const photoCards = document.querySelectorAll('.photo-card');
        const galleryOverlay = document.getElementById('gallery-overlay');
        let activeCard = null;
        const layouts = [[30, 38, 28, 5], [8, 18, 22, -12], [12, 60, 20, 10], [38, 8, 20, 18], [42, 72, 24, -15], [65, 12, 20, -8], [60, 45, 26, 3], [70, 70, 22, 16], [5, 42, 24, -2], [78, 25, 18, 11], [75, 58, 18, -10]];

        photoCards.forEach((card, index) => {
            if (index < layouts.length) {
                const [top, left, width, rotation] = layouts[index];
                card.style.top = `${top}%`;
                card.style.left = `${left}%`;
                card.style.width = `${width}vw`;
                card.style.maxWidth = '350px';
                card.style.height = 'auto';
                const transform = `rotate(${rotation}deg)`;
                card.style.transform = transform;
                card.dataset.originalTransform = transform;
                card.style.zIndex = index;
            }
            card.addEventListener('click', () => {
                if (galleryOverlay.classList.contains('active')) {
                    closeViewer();
                    return;
                }
                activeCard = card;
                galleryOverlay.classList.add('active');
                const cardRect = card.getBoundingClientRect();
                const viewportWidth = window.innerWidth;
                const viewportHeight = window.innerHeight;
                const scale = Math.min(viewportWidth / cardRect.width, viewportHeight / cardRect.height) * 0.9;
                const translateX = (viewportWidth / 2) - (cardRect.left + cardRect.width / 2);
                const translateY = (viewportHeight / 2) - (cardRect.top + cardRect.height / 2);
                card.style.transform = `translate(${translateX}px, ${translateY}px) scale(${scale}) rotate(0deg)`;
                card.classList.add('is-active');
            });
        });

        const closeViewer = () => {
            galleryOverlay.classList.remove('active');
            if (activeCard) {
                activeCard.style.transform = activeCard.dataset.originalTransform;
                setTimeout(() => {
                    if (activeCard) {
                        activeCard.classList.remove('is-active');
                        activeCard = null;
                    }
                }, 600);
            }
        };
        galleryOverlay.addEventListener('click', closeViewer);
    }

    if (whatsappFab) {
        whatsappFab.addEventListener('click', () => {
            const whatsappNumber = "201006129818";
            const defaultMessage = "Hello, I'm interested in your trips.";
            const whatsappURL = `https://wa.me/${whatsappNumber}?text=${encodeURIComponent(defaultMessage)}`;
            window.open(whatsappURL, "_blank");
        });
    }

    if (currentYearSpan) {
        currentYearSpan.textContent = new Date().getFullYear();
    }

    updateLanguage(currentLang);
});
