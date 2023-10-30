document.addEventListener('DOMContentLoaded', function () {
    $("#select-parsels").select2({
        theme: 'bootstrap-5',
        placeholder: "Выберите РПО",
        language: "ru",
        minimumInputLength: 1,
        maximumInputLength: 13,
        multiple: true,
        width: "100%",
        ajax: {
            url: '/Select2/Parcels',
            dataType: 'json',
            delay: 250,
            width: 'resolve',
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            }
        }
    });

    $("#select-users").select2({
        theme: 'bootstrap-5',
        placeholder: "Выберите ответственного",
        language: "ru",
        minimumInputLength: 1,
        multiple: false,
        width: "100%",
        ajax: {
            url: '/Select2/Users',
            dataType: 'json',
            delay: 250,
            width: 'resolve',
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            }
        }
    });

    $("#select-placeOfDeparture").select2({
        theme: 'bootstrap-5',
        placeholder: "Выберите место отправки",
        language: "ru",
        minimumInputLength: 1,
        multiple: false,
        width: "100%",
        ajax: {
            url: '/Select2/Places',
            dataType: 'json',
            delay: 250,
            width: 'resolve',
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            }
        }
    });

    $("#select-placeOfDelivery").select2({
        theme: 'bootstrap-5',
        placeholder: "Выберите место доставки",
        language: "ru",
        minimumInputLength: 1,
        multiple: false,
        width: "100%",
        ajax: {
            url: '/Select2/Places',
            dataType: 'json',
            delay: 250,
            width: 'resolve',
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            }
        }
    });

    $("#select-places").select2({
        theme: 'bootstrap-5',
        placeholder: "Выберите место работы",
        language: "ru",
        minimumInputLength: 1,
        multiple: false,
        width: "100%",
        ajax: {
            url: '/Select2/Places',
            dataType: 'json',
            delay: 250,
            width: 'resolve',
            data: function (params) {
                return {
                    term: params.term
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            }
        }
    });

    $("#select-militaryUnits").select2({
        theme: 'bootstrap-5',
        placeholder: "Выберите в/ч",
        language: "ru",
        minimumInputLength: 1,
        multiple: false,
        width: "100%",
        ajax: {
            url: '/Select2/MilitaryUnits',
            dataType: 'json',
            delay: 250,
            width: 'resolve',
            data: function (params) {
                let placeId = $('#select-placeOfDelivery').val();
                console.log(placeId);
                return {
                    term: params.term,
                    placeId: placeId
                };
            },
            processResults: function (data) {
                return {
                    results: data.items
                };
            }
        }
    });
});