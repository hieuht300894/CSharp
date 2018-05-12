function ShowModal(modal_id) {
    var modal = document.getElementById(modal_id);

    modal.setAttribute('class', 'modal fade in');
    modal.style.display = 'block';

    var modal_btnTopRightCloses = modal.getElementsByClassName('close');
    if (modal_btnTopRightCloses.length > 0) {
        modal_btnTopRightCloses[0].addEventListener('click', function () {
            CloseModal(modal_id);
        });
    }

    var modal_btnClose = document.getElementById('btnClose');
    if (modal_btnClose) {
        modal_btnClose[0].addEventListener('click', function () {
            CloseModal(modal_id);
        });
    }

    window.addEventListener('click', function (event) {
        if (event.target == modal) {
            CloseModal(modal_id);
        }
    });
}

function CloseModal(modal_id) {
    var modal = document.getElementById(modal_id);

    modal.setAttribute('class', 'modal fade');
    modal.style.display = 'none';

    var modal_backdrop_ins = document.getElementsByClassName('modal-backdrop in');
    if (modal_backdrop_ins.length > 0) {
        modal_backdrop_ins[0].setAttribute('class', '');
    }
}