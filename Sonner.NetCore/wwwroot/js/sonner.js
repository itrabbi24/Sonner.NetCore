class SonnerToaster {
    constructor() {
        this.container = document.createElement('div');
        this.container.className = 'sonner-toaster';
        document.body.appendChild(this.container);
    }

    getIconSvg(type) {
        if (type === 'Success') {
            return `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"><path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zm3.857-9.809a.75.75 0 00-1.214-.882l-3.483 4.79-1.88-1.88a.75.75 0 10-1.06 1.061l2.5 2.5a.75.75 0 001.137-.089l4-5.5z" clip-rule="evenodd" /></svg>`;
        }
        if (type === 'Error') {
            return `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"><path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.28 7.22a.75.75 0 00-1.06 1.06L8.94 10l-1.72 1.72a.75.75 0 101.06 1.06L10 11.06l1.72 1.72a.75.75 0 101.06-1.06L11.06 10l1.72-1.72a.75.75 0 00-1.06-1.06L10 8.94 8.28 7.22z" clip-rule="evenodd" /></svg>`;
        }
        if (type === 'Warning') {
             return `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"><path fill-rule="evenodd" d="M8.485 2.495c.673-1.167 2.357-1.167 3.03 0l6.28 10.875c.673 1.167-.17 2.625-1.516 2.625H3.72c-1.347 0-2.189-1.458-1.515-2.625L8.485 2.495zM10 5a.75.75 0 01.75.75v3.5a.75.75 0 01-1.5 0v-3.5A.75.75 0 0110 5zm0 9a1 1 0 100-2 1 1 0 000 2z" clip-rule="evenodd" /></svg>`;
        }
        return '';
    }

    toast(message, type = 'Default', title = null) {
        const toastEl = document.createElement('div');
        toastEl.className = `sonner-toast sonner-toast-${type.toLowerCase()}`;
        
        const iconSvg = this.getIconSvg(type);
        const iconHtml = iconSvg ? `<div class="sonner-toast-icon">${iconSvg}</div>` : '';
        
        const contentHtml = `
            <div class="sonner-toast-content">
                ${title ? `<span class="sonner-toast-title">${title}</span>` : ''}
                <span class="sonner-toast-message">${message}</span>
            </div>
        `;

        toastEl.innerHTML = iconHtml + contentHtml;
        this.container.appendChild(toastEl);

        // Trigger animation
        requestAnimationFrame(() => {
            requestAnimationFrame(() => {
                toastEl.classList.add('sonner-toast-visible');
            });
        });

        // Remove after 4 seconds
        setTimeout(() => {
            toastEl.classList.remove('sonner-toast-visible');
            toastEl.addEventListener('transitionend', () => {
                toastEl.remove();
            });
        }, 4000);
    }
}

// Global instance
window.sonner = new SonnerToaster();
