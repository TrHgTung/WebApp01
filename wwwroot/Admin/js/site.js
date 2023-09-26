$(function () {

    if ($("a.confirmDeletion").length) {
        $("a.confirmDeletion").click(() => {
            if (!confirm("Xác nhận xóa?")) return false;
        });
    }

    if ($("a.confirmImportantDeletion").length) {
        $("a.confirmImportantDeletion").click(() => {
            if (!confirm("Lưu ý! Thao tác xóa này có thể gây ảnh hưởng đến các sản phẩm thuộc danh mục / NXB. Vẫn muốn xóa?")) return false;
        });
    }

    if ($("div.alert.notification").length) {
        setTimeout(() => {
            $("div.alert.notification").fadeOut();
        }, 2000);
    }

});

function readURL(input) {
    if (input.files && input.files[0]) {
        let reader = new FileReader();

        reader.onload = function (e) {
            $("img#imgpreview").attr("src", e.target.result).width(200).height(200);
        };

        reader.readAsDataURL(input.files[0]);
    }
}