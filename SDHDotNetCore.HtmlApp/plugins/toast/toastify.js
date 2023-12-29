const successToast = document.querySelector('.btn-toast.btn-success-toast')
const infoToast = document.querySelector('.btn-toast.btn-info-toast')
const dangerToast = document.querySelector('.btn-toast.btn-danger-toast')
const warningToast = document.querySelector('.btn-toast.btn-warning-toast')
 
successToast.addEventListener('click', e=>{
    e.preventDefault()
    new CustomToast().show(`This is success toast notification.`, 'success', 10000)
})
 
infoToast.addEventListener('click', e=>{
    e.preventDefault()
    new CustomToast().show(`This is info toast notification.`, 'info', 10000)
})
 
dangerToast.addEventListener('click', e=>{
    e.preventDefault()
    new CustomToast().show(`This is danger toast notification.`, 'danger', 10000)
})
 
warningToast.addEventListener('click', e=>{
    e.preventDefault()
    new CustomToast().show(`This is warning toast notification.`, 'warning', 10000)
})