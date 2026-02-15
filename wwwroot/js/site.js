/* ============================================
   MamNonApp - Trường Mầm Non Hoa Hướng Dương
   Main JavaScript
   ============================================ */

document.addEventListener('DOMContentLoaded', function() {
    
    // Navbar scroll effect
    const navbar = document.getElementById('mainNav');
    if (navbar) {
        window.addEventListener('scroll', function() {
            if (window.scrollY > 50) {
                navbar.classList.add('scrolled');
            } else {
                navbar.classList.remove('scrolled');
            }
        });
    }

    // Active menu highlighting
    const currentPath = window.location.pathname;
    const navLinks = document.querySelectorAll('#navbarNav .nav-link');
    
    navLinks.forEach(link => {
        const href = link.getAttribute('href');
        if (href) {
            // Check if current path starts with the link href
            if (currentPath === href || 
                (href !== '/' && currentPath.startsWith(href))) {
                link.classList.add('active');
            } else {
                link.classList.remove('active');
            }
        }
    });

    // Back to top button
    const backToTopBtn = document.getElementById('backToTop');
    if (backToTopBtn) {
        window.addEventListener('scroll', function() {
            if (window.scrollY > 300) {
                backToTopBtn.style.display = 'block';
            } else {
                backToTopBtn.style.display = 'none';
            }
        });

        backToTopBtn.addEventListener('click', function() {
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });
    }

    // Smooth scroll for anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function(e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // Lazy loading images
    const lazyImages = document.querySelectorAll('img[loading="lazy"]');
    if ('IntersectionObserver' in window) {
        const imageObserver = new IntersectionObserver((entries, observer) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const img = entry.target;
                    img.classList.add('loaded');
                    observer.unobserve(img);
                }
            });
        });

        lazyImages.forEach(img => imageObserver.observe(img));
    }

    // Fade in animation on scroll
    const fadeElements = document.querySelectorAll('.program-card, .teacher-card, .testimonial-card, .value-card, .activity-card, .news-card');
    if ('IntersectionObserver' in window) {
        const fadeObserver = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add('fade-in');
                }
            });
        }, {
            threshold: 0.1
        });

        fadeElements.forEach(el => fadeObserver.observe(el));
    }

    // Gallery filter functionality (if exists)
    const galleryFilter = document.querySelector('.gallery-filter');
    if (galleryFilter) {
        const filterButtons = galleryFilter.querySelectorAll('.btn');
        const galleryItems = document.querySelectorAll('.gallery-item-wrapper');

        filterButtons.forEach(button => {
            button.addEventListener('click', function(e) {
                e.preventDefault();
                
                const category = this.dataset.category || '';
                
                galleryItems.forEach(item => {
                    const itemCategory = item.dataset.category;
                    if (!category || itemCategory === category) {
                        item.style.display = 'block';
                        setTimeout(() => {
                            item.style.opacity = '1';
                        }, 50);
                    } else {
                        item.style.opacity = '0';
                        setTimeout(() => {
                            item.style.display = 'none';
                        }, 300);
                    }
                });

                // Update active button
                filterButtons.forEach(btn => {
                    btn.classList.remove('btn-warning', 'text-white');
                    btn.classList.add('btn-outline-dark');
                });
                this.classList.remove('btn-outline-dark');
                this.classList.add('btn-warning', 'text-white');
            });
        });
    }

    // Form validation enhancement
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', function(e) {
            if (!form.checkValidity()) {
                e.preventDefault();
                e.stopPropagation();
            }
            form.classList.add('was-validated');
        });
    });

    // Phone number formatting
    const phoneInputs = document.querySelectorAll('input[type="tel"]');
    phoneInputs.forEach(input => {
        input.addEventListener('input', function(e) {
            let value = e.target.value.replace(/\D/g, '');
            if (value.length > 10) value = value.slice(0, 10);
            
            // Format: 0xxx-xxx-xxx
            if (value.length >= 4 && value.length < 7) {
                value = value.slice(0, 4) + '-' + value.slice(4);
            } else if (value.length >= 7) {
                value = value.slice(0, 4) + '-' + value.slice(4, 7) + '-' + value.slice(7);
            }
            
            e.target.value = value;
        });
    });

    // Counter animation for stats - FIXED to handle NaN
    const counters = document.querySelectorAll('.display-5');
    if ('IntersectionObserver' in window) {
        const counterObserver = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const counter = entry.target;
                    const text = counter.innerText.trim();
                    // Extract number from text (remove +, %, etc.)
                    const numMatch = text.match(/\d+/);
                    if (numMatch) {
                        const target = parseInt(numMatch[0]);
                        if (!isNaN(target)) {
                            animateCounter(counter, 0, target, 2000);
                        }
                    }
                    counterObserver.unobserve(counter);
                }
            });
        }, { threshold: 0.5 });

        counters.forEach(counter => counterObserver.observe(counter));
    }

    function animateCounter(element, start, end, duration) {
        let startTimestamp = null;
        const suffix = element.innerText.replace(/\d+/g, '').trim(); // Get the suffix (+, %, etc.)
        
        const step = (timestamp) => {
            if (!startTimestamp) startTimestamp = timestamp;
            const progress = Math.min((timestamp - startTimestamp) / duration, 1);
            const currentValue = Math.floor(progress * (end - start) + start);
            element.innerText = currentValue + suffix;
            if (progress < 1) {
                window.requestAnimationFrame(step);
            }
        };
        window.requestAnimationFrame(step);
    }

    // Newsletter form (if exists)
    const newsletterForm = document.querySelector('.newsletter-form');
    if (newsletterForm) {
        newsletterForm.addEventListener('submit', function(e) {
            e.preventDefault();
            const email = this.querySelector('input[type="email"]').value;
            if (email) {
                // Show success message
                showAlert('Cảm ơn bạn đã đăng ký nhận tin!', 'success');
                this.reset();
            }
        });
    }

    // Alert helper function
    function showAlert(message, type) {
        const alertDiv = document.createElement('div');
        alertDiv.className = `alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3`;
        alertDiv.style.zIndex = '9999';
        alertDiv.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;
        document.body.appendChild(alertDiv);
        
        setTimeout(() => {
            alertDiv.remove();
        }, 5000);
    }

    // Initialize Bootstrap tooltips
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function(tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Initialize Bootstrap popovers
    const popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
    popoverTriggerList.map(function(popoverTriggerEl) {
        return new bootstrap.Popover(popoverTriggerEl);
    });
});

// Export functions for global use
window.MamNonApp = {
    showAlert: function(message, type = 'info') {
        const alertDiv = document.createElement('div');
        alertDiv.className = `alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3`;
        alertDiv.style.zIndex = '9999';
        alertDiv.innerHTML = `
            ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        `;
        document.body.appendChild(alertDiv);
        
        setTimeout(() => {
            alertDiv.remove();
        }, 5000);
    },
    
    scrollToElement: function(selector) {
        const element = document.querySelector(selector);
        if (element) {
            element.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
    }
};
