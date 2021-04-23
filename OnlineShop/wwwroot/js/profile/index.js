


const showOrHideUploadImageButton = () => {
    const form = $("#form-upload");
    const btnShow = $("#btnUpload");
    if (form.is(":visible")) {
        form.hide();
        btnShow.show();
    } else {
        form.show();
        btnShow.hide();
    }

}
