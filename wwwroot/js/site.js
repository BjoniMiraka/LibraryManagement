// Global Toast/Alert System
// Use this function everywhere for consistent notifications

function showToast(message, type = 'info', duration = 4000) {
    // Get or create toast container
    let container = document.getElementById('toast-container');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toast-container';
        container.style.cssText = 'position: fixed; top: 80px; right: 20px; z-index: 9999; min-width: 350px; max-width: 500px;';
        document.body.appendChild(container);
    }

    // Create unique ID for this toast
    const toastId = 'toast-' + Date.now() + '-' + Math.random().toString(36).substr(2, 9);

    // Icon based on type
    const icons = {
        success: '<i class="bi bi-check-circle-fill me-2"></i>',
        danger: '<i class="bi bi-x-circle-fill me-2"></i>',
        warning: '<i class="bi bi-exclamation-triangle-fill me-2"></i>',
        info: '<i class="bi bi-info-circle-fill me-2"></i>'
    };

    // Create toast HTML
    const toastHtml = `
        <div id="${toastId}" class="alert alert-${type} alert-dismissible fade show shadow-lg mb-2" role="alert" style="animation: slideInRight 0.3s ease-out; white-space: pre-line;">
            ${icons[type] || icons.info}${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
        </div>
    `;

    // Add to container
    container.insertAdjacentHTML('beforeend', toastHtml);

    // Auto-remove after duration
    setTimeout(function() {
        const toast = document.getElementById(toastId);
        if (toast) {
            toast.classList.remove('show');
            toast.classList.add('fade-out');
            setTimeout(function() {
                toast.remove();
            }, 300);
        }
    }, duration);
}

// Add CSS animations
const style = document.createElement('style');
style.textContent = `
    @keyframes slideInRight {
        from {
            transform: translateX(100%);
            opacity: 0;
        }
        to {
            transform: translateX(0);
            opacity: 1;
        }
    }
    
    .fade-out {
        animation: fadeOut 0.3s ease-out;
    }
    
    @keyframes fadeOut {
        from {
            opacity: 1;
        }
        to {
            opacity: 0;
            transform: translateX(100%);
        }
    }
`;
document.head.appendChild(style);

// Backward compatibility - map old showAlert to new showToast
window.showAlert = showToast;
