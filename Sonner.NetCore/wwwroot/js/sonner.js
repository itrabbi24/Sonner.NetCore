class SonnerToaster {
    constructor(options = {}) {
        this.options = {
            position: 'bottom-right',
            expand: false,
            richColors: false,
            closeButton: false,
            theme: 'light',
            ...options
        };

        this.toasts = []; // This will now store { element, position }
        this.containers = {}; // Map of position -> container element

        this.getOrCreateContainer(this.options.position);
    }

    getOrCreateContainer(position) {
        if (this.containers[position]) return this.containers[position];

        const container = document.createElement('div');
        container.className = `sonner-toaster sonner-toaster-${position}`;
        container.setAttribute('data-theme', this.options.theme);
        if (this.options.expand) {
            container.classList.add('sonner-toaster-expand');
        }

        document.body.appendChild(container);

        // Listen for mouse enter/leave to expand/shrink
        container.addEventListener('mouseenter', () => {
            if (!this.options.expand) {
                container.classList.add('sonner-toaster-expand');
            }
        });

        container.addEventListener('mouseleave', () => {
            if (!this.options.expand) {
                container.classList.remove('sonner-toaster-expand');
            }
        });

        this.containers[position] = container;
        return container;
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
        if (type === 'Info') {
            return `<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor"><path fill-rule="evenodd" d="M18 10a8 8 0 11-16 0 8 8 0 0116 0zm-7-4a1 1 0 11-2 0 1 1 0 012 0zM9 9a.75.75 0 000 1.5h.253a.25.25 0 01.244.304l-.459 2.066A1.75 1.75 0 0010.747 15H11a.75.75 0 000-1.5h-.253a.25.25 0 01-.244-.304l.459-2.066A1.75 1.75 0 009.253 9H9z" clip-rule="evenodd" /></svg>`;
        }
        return '';
    }

    toast(message, type = 'Default', title = null, position = null) {
        const toastPos = position || this.options.position;
        const container = this.getOrCreateContainer(toastPos);

        const toastEl = document.createElement('div');
        toastEl.className = `sonner-toast sonner-toast-${type.toLowerCase()}`;
        if (this.options.richColors) {
            toastEl.classList.add('sonner-toast-rich-colors');
        }

        const iconSvg = this.getIconSvg(type);
        const iconHtml = iconSvg ? `<div class="sonner-toast-icon">${iconSvg}</div>` : '';

        let closeHtml = '';
        if (this.options.closeButton) {
            closeHtml = `
                <button class="sonner-toast-close" onclick="this.parentElement.remove()">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20" fill="currentColor" width="12" height="12"><path d="M6.28 5.22a.75.75 0 00-1.06 1.06L8.94 10l-3.72 3.72a.75.75 0 101.06 1.06L10 11.06l3.72 3.72a.75.75 0 101.06-1.06L11.06 10l3.72-3.72a.75.75 0 00-1.06-1.06L10 8.94 6.28 5.22z" /></svg>
                </button>
            `;
        }

        const contentHtml = `
            <div class="sonner-toast-content">
                ${title ? `<span class="sonner-toast-title">${title}</span>` : ''}
                <span class="sonner-toast-message">${message}</span>
            </div>
        `;

        toastEl.innerHTML = iconHtml + contentHtml + closeHtml;

        // Add to list and container
        this.toasts.unshift({ element: toastEl, position: toastPos });
        if (toastPos.startsWith('top')) {
            container.prepend(toastEl);
        } else {
            container.appendChild(toastEl);
        }

        // Apply indexing for stacking
        this.updateToastIndices(toastPos);

        // Trigger animation
        requestAnimationFrame(() => {
            requestAnimationFrame(() => {
                toastEl.classList.add('sonner-toast-visible');
            });
        });

        // Remove after 4 seconds
        setTimeout(() => {
            this.removeToast(toastEl, toastPos);
        }, 4000);
    }

    removeToast(toastEl, position) {
        toastEl.classList.remove('sonner-toast-visible');
        toastEl.classList.add('sonner-toast-hiding');

        const onEnd = () => {
            toastEl.remove();
            this.toasts = this.toasts.filter(t => t.element !== toastEl);
            this.updateToastIndices(position);
        };

        toastEl.addEventListener('transitionend', onEnd, { once: true });
        // Fallback
        setTimeout(onEnd, 400);
    }

    updateToastIndices(position) {
        const toastsInPos = this.toasts.filter(t => t.position === position);
        toastsInPos.forEach((toast, index) => {
            toast.element.setAttribute('data-index', index);
            toast.element.style.setProperty('--index', index);
            toast.element.style.setProperty('--toasts-before', index);
            toast.element.style.setProperty('--z-index', 1000 - index);
        });
    }
}

// Global instance (fallback for older tag helper style)
if (!window.sonner) {
    window.sonner = new SonnerToaster();
}
