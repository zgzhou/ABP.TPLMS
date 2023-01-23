(function() {
    $(function() {

        var _moduleService = abp.services.app.moduleAbp;
        var _$modal = $('#ModuleCreateModal');
        var _$form = _$modal.find('form');

        _$form.validate({          
        });

        $('#RefreshButton').click(function () {
            refreshModuleList();
        });

        $('.delete-user').click(function () {
            var userId = $(this).attr("data-user-id");
            var userName = $(this).attr('data-user-name');

            deleteUser(userId, userName);
        });

        $('.edit-user').click(function (e) {
            var userId = $(this).attr("data-user-id");

            e.preventDefault();
            $.ajax({
                url: abp.appPath + 'Users/EditUserModal?userId=' + userId,
                type: 'POST',
                contentType: 'application/html',
                success: function (content) {
                    $('#UserEditModal div.modal-content').html(content);
                },
                error: function (e) { }
            });
        });

        _$form.find('button[type="submit"]').click(function (e) {
            e.preventDefault();

            if (!_$form.valid()) {
                return;
            }

            var module = _$form.serializeFormToObject(); //serializeFormToObject is defined in main.js
          
            abp.ui.setBusy(_$modal);
            _moduleService.create(module).done(function () {
                _$modal.modal('hide');
                location.reload(true); //reload page to see new user!
            }).always(function () {
                abp.ui.clearBusy(_$modal);
            });
        });
        
        _$modal.on('shown.bs.modal', function () {
            _$modal.find('input:not([type=hidden]):first').focus();
        });

        function refreshModuleList() {
            location.reload(true); //reload page to see new user!
        }

        function deleteModule(moduleId, moduleName) {
            abp.message.confirm(
                abp.utils.formatString(abp.localization.localize('AreYouSureWantToDelete', 'TPLMS'), moduleName),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _moduleService.delete({
                            id: moduleId
                        }).done(function () {
                            refreshModuleList();
                        });
                    }
                }
            );
        }
    });
})();