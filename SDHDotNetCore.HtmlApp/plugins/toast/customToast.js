class CustomToast{
    // Toast Box variable
    toastBox;
    // Toast Box Duration variable
    duration;
    // Valid Toast Icons
    toastIcon = {
        success: `<span class="material-symbols-outlined">task_alt</span>`,
        danger: `<span class="material-symbols-outlined">error</span>`,
        warning: `<span class="material-symbols-outlined">warning</span>`,
        info: `<span class="material-symbols-outlined">info</span>`
    };
    show(message="Sample Message", toastType="info", duration = 5000){
        // Check if toast type is valid, otherwise make info toast as the default 
        if(!Object.keys(this.toastIcon).includes(toastType))
            toastType = `info`;
        // Creatign the Toast Box Element
        this.toastBox = document.createElement('div')
        // Adding .toast class to Toast Box Element
        this.toastBox.classList.add('toast', `toast-${toastType}`)
        // Toast box content
        this.toastBox.innerHTML = `<button class="toast-close-btn"><span class="material-symbols-outlined">close</span></button>
            <div class="toast-content-wrapper">
                <div class="toast-icon">
                    ${this.toastIcon[toastType]}
                </div>
                <div class="toast-message">${message}</div>
                <div class="toast-progress"></div>
            </div>`;
        // Set Toast Duration
        this.duration = duration
        // Update Toast Duration
        this.toastBox.querySelector('.toast-progress').style.animationDuration = `${this.duration / 1000}s`
 
        // Remove Current Toast Notification if Exists
        if(document.body.querySelector('.toast') != null)
            document.body.querySelector('.toast').remove();
        // Append New Toast Notification to the document
        document.body.appendChild(this.toastBox)
        // Trigger closing duration
        this.triggerClose()
        // When Close Icon/Button is clicked event listener
        this.toastBox.querySelector('.toast-close-btn').addEventListener('click', e=>{
            e.preventDefault()
            // Trigger immediate closing
            this.triggerClose(0)
        })
    }
    triggerClose(closeDuration = null){
        // Set toast duration as the closing delay if the closing duration value is null
        if(closeDuration == null){
            closeDuration=this.duration
        }
        setTimeout(()=>{
            // adding closing class for closing animation
            this.toastBox.classList.add('closing')
            // trigger removing the toast notification
            this.closeToast()
        },closeDuration)
    }
    closeToast(){
        // Set removing toast delay
        setTimeout(()=>{
            this.toastBox.remove();
        }, 500)
    }
}