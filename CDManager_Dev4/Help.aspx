<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Front.Master" AutoEventWireup="true" CodeBehind="Help.aspx.cs" Inherits="CDManager_Dev4.Help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    快速上手指引
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin-top: 30px; margin-right: 20px; margin-left: 20px; margin-bottom: 50px; width: 100%;">
        <h2 style="text-align: center">快速上手指引</h2>

        <div style="margin-right: 20%; margin-left: 20%; margin-top: 50px">
<hr />
            <p>
                尊敬的老师、同学，您好：
            </p>
            <p style="text-indent: 2em;">
                欢迎使用广州大学华软软件学院图书馆光盘查询系统，为了更好的为老师和同学们服务，更快地使熟悉本系统的基本使用方法，我们收集了本系统的快速上手及使用问题<span style="color: #CC0000; font-weight: bold">(点击标题即可查看相关说明。若无法打开，请下载读者使用文档查看，<a href="光盘系统读者使用说明书.doc">点击下载</a>)</span>，如果以下各项内容不能解决您对使用本系统的疑问，欢迎向我们咨询
            </p>
            <hr />
            <div id="help">
                <div id="q1_title" class="help_title">
                    我能使用什么关键字查询到我想要找到的图书或者光盘?
                </div>
                <div id="q1_detail" class="help_detail">
                    读者可通过对ISBN、图书名、出版社、作者等信息作为关键字查询图书，并可根据这些信息进行图书搜索结果排序；<br />
                    读者可通过对光盘序号、光盘名称、在线状态等信息作为关键字查询光盘，并可根据这些信息进行光盘搜索结果排序，并且可以在搜索结果中进行快速申请和下载操作
                </div>
                <div id="q2_title" class="help_title">
                    我如何能下载到自己想要的光盘资源?
                </div>
                <div id="q2_detail" class="help_detail">
                    读者通过以下步骤的操作即可下载到想要的光盘资源:
                    <ul>
                        <li>通过图书搜索下载光盘:
                            <ul>
                                <li>1.搜索自己想要下载的图书的信息<br />
                                    <img src="Resources/Images/20121109200613.jpg" />
                                </li>
                                <li>2.点击图书标题进入图书明细<br />
                                    若光盘资源不在线，则点击申请操作，耐心等待图书管理员上传光盘资源<br />
                                    <img src="Resources/Images/20121109201459.jpg" /><br />
                                    若光盘资源在线，则显示下载链接，读者可以直接下载<br />
                                    <img src="Resources/Images/20121109204201.jpg" />
                                </li>
                            </ul>
                        </li>
                        <li>通过光盘搜索下载光盘:
                            <ul>
                                <li>1.搜索自己想要下载的光盘信息<br />
                                    <img src="Resources/Images/20121109204731.jpg" />
                                </li>
                                <li>2.如果光盘资源在线,点击下载按钮便可直接下载光盘资源；如果不在线，点击申请按钮即可提交光盘资源申请<br />
                                    <img src="Resources/Images/20121109204928.jpg" />
                                </li>
                            </ul>
                        </li>
                    </ul>

                </div>
                <div id="q3_title" class="help_title">
                    如果我想下载的光盘资源不在线，那么我如何得知我提交的光盘资源申请的处理结果？
                </div>
                <div id="q3_detail" class="help_detail">
                    读者在发送光盘资源申请后，图书管理员会根据服务器空间，光盘文件大小及其他客观条件判断是否上传光盘资源，无论处理结果如何，读者都会在消息箱中收到申请的处理结果通知<br />
                    <img src="Resources/Images/20121109205604.jpg" />
                </div>
                <div id="q4_title" class="help_title">
                    为什么我明明在找到相关图书，却不能在本系统搜索光盘资源？
                </div>
                <div id="q4_detail" class="help_detail">
                    广州大学华软软件学院图书馆光盘查询系统只包含截止当日的最新的包含光盘的图书数据，如果读者搜索不到相关图书，原因有以下几点：
                    <ul>
                        <li>读者拿到的图书是较新入库的，相关数据并未导入到本系统
                        </li>
                        <li>读者拿到的图书并不包含光盘
                        </li>
                        <li>读者拿到的图书信息已经过期
                        </li>
                    </ul>
                </div>
                <div id="q5_title" class="help_title">
                    一位读者最多能下载多少张光盘的资源？
                </div>
                <div id="q5_detail" class="help_detail">
                    本系统并未对读者下载的资源的总数作限制,读者可下载的光盘资源数量是没有上限的
                </div>
                <div id="q6_title" class="help_title">
                    我的登录账号密码是什么?忘记密码该如何找回密码？
                </div>
                <div id="q6_detail" class="help_detail">
                    读者的登录账号为10位的学生学号或者4位的职工工号，登录密码默认为000000，请读者尽快修改自己的登录密码以保证相关信息的安全；<br />
                    若读者忘记登录密码，忘记可凭本人学生证或者一卡通到图书馆借还书处办理密码找回手续。
                </div>
            </div>
            <hr />
        </div>

    </div>
</asp:Content>
