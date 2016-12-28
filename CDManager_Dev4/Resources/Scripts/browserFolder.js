function BrowseServer() {
    try {
        var Message = "请选择文件夹";
        var Shell = new ActiveXObject("Shell.Application");
        var Folder = Shell.BrowseForFolder(0, Message, 0x0040, 0x0011); //起始目录为：我的电脑
        if (Folder != null) {
            Folder = Folder.items(); // 返回 FolderItems 对象
            Folder = Folder.item();
            Folder = Folder.Path; // 返回路径
            if (Folder.charAt(Folder.length - 1) != "\\") {
                Folder = Folder + "\\";
            }
            var bb = document.getElementById("MainContent_txtFTPServer");
            //document.getElementByIdx_x("BackupPath").value = Folder;
            bb.value = Folder;
            return Folder;
        }
    } catch (e) {
        alert(e.message);
    }
}

function BrowseHome() {
    try {
        var Message = "请选择文件夹";
        var Shell = new ActiveXObject("Shell.Application");
        var Folder = Shell.BrowseForFolder(0, Message, 0x0040, 0x0011); //起始目录为：我的电脑
        if (Folder != null) {
            Folder = Folder.items(); // 返回 FolderItems 对象
            Folder = Folder.item();
            Folder = Folder.Path; // 返回路径
            if (Folder.charAt(Folder.length - 1) != "\\") {
                Folder = Folder + "\\";
            }
            var bb = document.getElementById("MainContent_txtFTPHome");
            //document.getElementByIdx_x("BackupPath").value = Folder;
            bb.value = Folder;
            return Folder;
        }
    } catch (e) {
        alert(e.message);
    }
}