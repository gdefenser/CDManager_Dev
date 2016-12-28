<%@ Page Title="" Language="C#" MasterPageFile="~/Resources/Masters/Front.Master" AutoEventWireup="true" CodeBehind="IndexFill.aspx.cs" Inherits="CDManager_Dev4.IndexFill" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    图书分类目录
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upIndexFill" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td width="20%" style="vertical-align: top;">
                        <div style=" width:300px;border-color: #5381AC; border-width: 1px; border-style: solid;height:800px;overflow:scroll">
                            <asp:TreeView ID="tvType" runat="server" ExpandDepth="0" OnSelectedNodeChanged="tvType_SelectedNodeChanged">
                                <Nodes>
                                    <asp:TreeNode Text="A 马、列、毛、邓" Value="A">
                                        <asp:TreeNode Text="A1 马克思、恩格斯著作" Value="A1"></asp:TreeNode>
                                        <asp:TreeNode Text="A2 列宁著作" Value="A2"></asp:TreeNode>
                                        <asp:TreeNode Text="A3 斯大林著作" Value="A3"></asp:TreeNode>
                                        <asp:TreeNode Text="A4 毛泽东著作" Value="A4"></asp:TreeNode>
                                        <asp:TreeNode Text="A5 马克思、恩格斯、列宁、斯大林、毛泽东、邓小平著作汇编" Value="A5"></asp:TreeNode>
                                        <asp:TreeNode Text="A7 马克思、恩格斯、列宁、斯大林、毛泽东、邓小平生平和传记" Value="A7"></asp:TreeNode>
                                        <asp:TreeNode Text="A8 马克思主义、列宁主义、毛泽东思想 、邓小平理论的学习和研究" Value="A8"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="B 哲学、宗教" Value="B">
                                        <asp:TreeNode Text="B1 世界哲学" Value="B1"></asp:TreeNode>
                                        <asp:TreeNode Text="B2 中国哲学" Value="B2"></asp:TreeNode>
                                        <asp:TreeNode Text="B3 亚洲哲学" Value="B3"></asp:TreeNode>
                                        <asp:TreeNode Text="B4 非洲哲学" Value="B4"></asp:TreeNode>
                                        <asp:TreeNode Text="B5 欧洲哲学" Value="B5"></asp:TreeNode>
                                        <asp:TreeNode Text="B6 大洋洲哲学" Value="B6"></asp:TreeNode>
                                        <asp:TreeNode Text="B7 美洲哲学" Value="B7"></asp:TreeNode>
                                        <asp:TreeNode Text="B9 宗教" Value="B9"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="C 社会科学总论" Value="C">
                                        <asp:TreeNode Text="C1 社会科学现状及发展" Value="C1"></asp:TreeNode>
                                        <asp:TreeNode Text="C2 团体、会议、社会科学机构" Value="C2"></asp:TreeNode>
                                        <asp:TreeNode Text="C3 社会科学研究方法" Value="C3"></asp:TreeNode>
                                        <asp:TreeNode Text="C4 社会科学教育与普及" Value="C4"></asp:TreeNode>
                                        <asp:TreeNode Text="C5 社会科学丛书、文集、连续性出版物" Value="C5"></asp:TreeNode>
                                        <asp:TreeNode Text="C6 社会科学参考工具书" Value="C6"></asp:TreeNode>
                                        <asp:TreeNode Text="C7 社会科学文献检索工具书" Value="C7"></asp:TreeNode>
                                        <asp:TreeNode Text="C8 统计学" Value="C8"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="D 政治、法律" Value="D">
                                        <asp:TreeNode Text="D1 国际共产主义运动" Value="D1"></asp:TreeNode>
                                        <asp:TreeNode Text="D2 中国共产党" Value="D2"></asp:TreeNode>
                                        <asp:TreeNode Text="D4 工人、农民、青年、妇女运动与组织" Value="D4"></asp:TreeNode>
                                        <asp:TreeNode Text="D5 世界政治" Value="D5"></asp:TreeNode>
                                        <asp:TreeNode Text="D6 中国政治" Value="D6"></asp:TreeNode>
                                        <asp:TreeNode Text="D8 外交、国际关系" Value="D8"></asp:TreeNode>
                                        <asp:TreeNode Text="D9 法律" Value="D9"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="E 军事" Value="E">
                                        <asp:TreeNode Text="E1 世界军事" Value="E1"></asp:TreeNode>
                                        <asp:TreeNode Text="E2 中国军事" Value="E2"></asp:TreeNode>
                                        <asp:TreeNode Text="E3 E3/7  各国军事" Value="E3"></asp:TreeNode>
                                        <asp:TreeNode Text="E8 战略学、战役学、战术学" Value="E8"></asp:TreeNode>
                                        <asp:TreeNode Text="E9 军事技术" Value="E9"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="F 经济" Value="F">
                                        <asp:TreeNode Text="F1 世界各国经济概况、经济史、经济地理" Value="F1"></asp:TreeNode>
                                        <asp:TreeNode Text="F2 经济计划与管理" Value="F2"></asp:TreeNode>
                                        <asp:TreeNode Text="F3 农业经济" Value="F3"></asp:TreeNode>
                                        <asp:TreeNode Text="F4 工业经济" Value="F4"></asp:TreeNode>
                                        <asp:TreeNode Text="F5 交通运输经济" Value="F5"></asp:TreeNode>
                                        <asp:TreeNode Text="F6 邮电经济" Value="F6"></asp:TreeNode>
                                        <asp:TreeNode Text="F7 贸易经济" Value="F7"></asp:TreeNode>
                                        <asp:TreeNode Text="F8 财政、金融" Value="F8"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="G 文化、科学、教育、体育" Value="G">
                                        <asp:TreeNode Text="G1 世界各国文化与文化事业" Value="G1"></asp:TreeNode>
                                        <asp:TreeNode Text="G2 信息与知识传播" Value="G2"></asp:TreeNode>
                                        <asp:TreeNode Text="G3 科学、科学研究" Value="G3"></asp:TreeNode>
                                        <asp:TreeNode Text="G4 教育" Value="G4"></asp:TreeNode>
                                        <asp:TreeNode Text="G8 体育" Value="G8"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="H 语言、文字" Value="H">
                                        <asp:TreeNode Text="H1 汉语" Value="H1"></asp:TreeNode>
                                        <asp:TreeNode Text="H2 中国少数民族语言" Value="H2"></asp:TreeNode>
                                        <asp:TreeNode Text="H3 常用外国语" Value="H3"></asp:TreeNode>
                                        <asp:TreeNode Text="H4 汉藏语系" Value="H4"></asp:TreeNode>
                                        <asp:TreeNode Text="H5 阿尔泰语系（突厥-蒙古-通古斯语系）" Value="H5"></asp:TreeNode>
                                        <asp:TreeNode Text="H7 印欧语系" Value="H7"></asp:TreeNode>
                                        <asp:TreeNode Text="H9 国际辅助语" Value="H9"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="I 文学" Value="I">
                                        <asp:TreeNode Text="I1 世界文学" Value="I1"></asp:TreeNode>
                                        <asp:TreeNode Text="I2 中国文学" Value="I2"></asp:TreeNode>
                                        <asp:TreeNode Text="I3 I3/7  各国文学" Value="I3"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="J 艺术" Value="J">
                                        <asp:TreeNode Text="J1 世界各国艺术概况" Value="J1"></asp:TreeNode>
                                        <asp:TreeNode Text="J2 绘画" Value="J2"></asp:TreeNode>
                                        <asp:TreeNode Text="J3 雕塑" Value="J3"></asp:TreeNode>
                                        <asp:TreeNode Text="J4 摄影艺术" Value="J4"></asp:TreeNode>
                                        <asp:TreeNode Text="J5 工艺美术" Value="J5"></asp:TreeNode>
                                        <asp:TreeNode Text="J6 音乐" Value="J6"></asp:TreeNode>
                                        <asp:TreeNode Text="J7 舞蹈" Value="J7"></asp:TreeNode>
                                        <asp:TreeNode Text="J8 戏剧艺术" Value="J8"></asp:TreeNode>
                                        <asp:TreeNode Text="J9 电影、电视艺术" Value="J9"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="K 历史、地理" Value="K">
                                        <asp:TreeNode Text="K1 世界史" Value="K1"></asp:TreeNode>
                                        <asp:TreeNode Text="K2 中国史" Value="K2"></asp:TreeNode>
                                        <asp:TreeNode Text="K3 亚洲史" Value="K3"></asp:TreeNode>
                                        <asp:TreeNode Text="K4 非洲史" Value="K4"></asp:TreeNode>
                                        <asp:TreeNode Text="K5 欧洲史" Value="K5"></asp:TreeNode>
                                        <asp:TreeNode Text="K6 大洋洲史" Value="K6"></asp:TreeNode>
                                        <asp:TreeNode Text="K7 美洲史" Value="K7"></asp:TreeNode>
                                        <asp:TreeNode Text="K9 地理" Value="K9"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="N 自然科学总论" Value="N">
                                        <asp:TreeNode Text="N1 自然科学现状及发展" Value="N1"></asp:TreeNode>
                                        <asp:TreeNode Text="N2 自然科学机构、团体、会议" Value="N2"></asp:TreeNode>
                                        <asp:TreeNode Text="N3 自然科学研究方法" Value="N3"></asp:TreeNode>
                                        <asp:TreeNode Text="N4 自然科学教育与普及" Value="N4"></asp:TreeNode>
                                        <asp:TreeNode Text="N5 自然科学丛书、文集、连续性出版物" Value="N5"></asp:TreeNode>
                                        <asp:TreeNode Text="N6 自然科学参考工具书" Value="N6"></asp:TreeNode>
                                        <asp:TreeNode Text="N7 自然科学文献检索工具" Value="N7"></asp:TreeNode>
                                        <asp:TreeNode Text="N8 自然科学调查、考察" Value="N8"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="O 数理科学和化学" Value="O">
                                        <asp:TreeNode Text="O1 数学" Value="O1"></asp:TreeNode>
                                        <asp:TreeNode Text="O3 力学" Value="O3"></asp:TreeNode>
                                        <asp:TreeNode Text="O4 物理学" Value="O4"></asp:TreeNode>
                                        <asp:TreeNode Text="O6 化学" Value="O6"></asp:TreeNode>
                                        <asp:TreeNode Text="O7 几何晶体学" Value="O7"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="P 天文学,地球科学" Value="P">
                                        <asp:TreeNode Text="P1 天文学" Value="P1"></asp:TreeNode>
                                        <asp:TreeNode Text="P2 测绘学" Value="P2"></asp:TreeNode>
                                        <asp:TreeNode Text="P3 地球物理学" Value="P3"></asp:TreeNode>
                                        <asp:TreeNode Text="P4 大气科学(气象学)" Value="P4"></asp:TreeNode>
                                        <asp:TreeNode Text="P5 地质学" Value="P5"></asp:TreeNode>
                                        <asp:TreeNode Text="P7 海洋学" Value="P7"></asp:TreeNode>
                                        <asp:TreeNode Text="P9 自然地理学" Value="P9"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="Q 生物科学" Value="Q">
                                        <asp:TreeNode Text="Q1 普通生物学" Value="Q1"></asp:TreeNode>
                                        <asp:TreeNode Text="Q2 细胞学" Value="Q2"></asp:TreeNode>
                                        <asp:TreeNode Text="Q3 遗传学" Value="Q3"></asp:TreeNode>
                                        <asp:TreeNode Text="Q4 生理学" Value="Q4"></asp:TreeNode>
                                        <asp:TreeNode Text="Q5 生物化学" Value="Q5"></asp:TreeNode>
                                        <asp:TreeNode Text="Q6 生物物理学" Value="Q6"></asp:TreeNode>
                                        <asp:TreeNode Text="Q7 分子生物学" Value="Q7"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="R 医药,卫生" Value="R">
                                        <asp:TreeNode Text="R1 预防医学,卫生学" Value="R1"></asp:TreeNode>
                                        <asp:TreeNode Text="R2 中国医学" Value="R2"></asp:TreeNode>
                                        <asp:TreeNode Text="R3 基础医学" Value="R3"></asp:TreeNode>
                                        <asp:TreeNode Text="R4 临床医学" Value="R4"></asp:TreeNode>
                                        <asp:TreeNode Text="R5 内科学" Value="R5"></asp:TreeNode>
                                        <asp:TreeNode Text="R6 外科学" Value="R6"></asp:TreeNode>
                                        <asp:TreeNode Text="R8 特种医学" Value="R8"></asp:TreeNode>
                                        <asp:TreeNode Text="R9 药学" Value="R9"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="S 农业科学" Value="S">
                                        <asp:TreeNode Text="S1 农业基础科学" Value="S1"></asp:TreeNode>
                                        <asp:TreeNode Text="S2 农业工程" Value="S2"></asp:TreeNode>
                                        <asp:TreeNode Text="S3 农学(农艺学)" Value="S3"></asp:TreeNode>
                                        <asp:TreeNode Text="S4 植物保护" Value="S4"></asp:TreeNode>
                                        <asp:TreeNode Text="S5 农作物" Value="S5"></asp:TreeNode>
                                        <asp:TreeNode Text="S6 园艺" Value="S6"></asp:TreeNode>
                                        <asp:TreeNode Text="S7 林业" Value="S7"></asp:TreeNode>
                                        <asp:TreeNode Text="S8 畜牧、动物医学、狩猎、蚕、蜂" Value="S8"></asp:TreeNode>
                                        <asp:TreeNode Text="S9 水产、渔业" Value="S9"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="T 工业技术" Value="T">
                                        <asp:TreeNode Text="T-0 工业技术理论" Value="T-0"></asp:TreeNode>
                                        <asp:TreeNode Text="T-1 工业技术现状与发展" Value="T-1"></asp:TreeNode>
                                        <asp:TreeNode Text="T-2 机构、团体、会议" Value="T-2"></asp:TreeNode>
                                        <asp:TreeNode Text="T-6 参考工具书" Value="T-6"></asp:TreeNode>
                                        <asp:TreeNode Text="T-9 工业经济" Value="T-9"></asp:TreeNode>
                                        <asp:TreeNode Text="TB 一般工业技术-工业技术" Value="TB"></asp:TreeNode>
                                        <asp:TreeNode Text="TE 石油、天然气工业TD 矿业工程" Value="TE"></asp:TreeNode>
                                        <asp:TreeNode Text="TF 冶金工业" Value="TF"></asp:TreeNode>
                                        <asp:TreeNode Text="TG 金属学与金属工艺" Value="TG"></asp:TreeNode>
                                        <asp:TreeNode Text="TH 机械、仪表工业" Value="TH"></asp:TreeNode>
                                        <asp:TreeNode Text="TJ 武器工业" Value="TJ"></asp:TreeNode>
                                        <asp:TreeNode Text="TK 能源与动力工程" Value="TK"></asp:TreeNode>
                                        <asp:TreeNode Text="TL 原子能技术" Value="TL"></asp:TreeNode>
                                        <asp:TreeNode Text="TM 电子技术" Value="TM"></asp:TreeNode>
                                        <asp:TreeNode Text="TN 无线电电子学、电信技术" Value="TN"></asp:TreeNode>
                                        <asp:TreeNode Text="TP 自动化技术计算技术技术" Value="TP"></asp:TreeNode>
                                        <asp:TreeNode Text="TQ 化学工业" Value="TQ"></asp:TreeNode>
                                        <asp:TreeNode Text="TS 轻工业、手工业" Value="TS"></asp:TreeNode>
                                        <asp:TreeNode Text="TU 建筑科学" Value="TU"></asp:TreeNode>
                                        <asp:TreeNode Text="TV 水利工程" Value="TV"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="U 交通运输" Value="U 交通运输">
                                        <asp:TreeNode Text="U1 综合运输" Value="U1"></asp:TreeNode>
                                        <asp:TreeNode Text="U2 铁路运输" Value="U2"></asp:TreeNode>
                                        <asp:TreeNode Text="U4 公路运输" Value="U4"></asp:TreeNode>
                                        <asp:TreeNode Text="U6 水路运输" Value="U6"></asp:TreeNode>
                                        <asp:TreeNode Text="U8 航空运输" Value="U8"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="V 航空航天" Value="V">
                                        <asp:TreeNode Text="V1 航空,航天技术的研究与探索" Value="V1"></asp:TreeNode>
                                        <asp:TreeNode Text="V2 航空" Value="V2"></asp:TreeNode>
                                        <asp:TreeNode Text="V4 航天(宇宙航行)" Value="V4"></asp:TreeNode>
                                        <asp:TreeNode Text="V7 航空航天医学" Value="V7"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="X 环境科学,安全科学" Value="X">
                                        <asp:TreeNode Text="X1 环境科学基础理论" Value="X1"></asp:TreeNode>
                                        <asp:TreeNode Text="X2 社会与环境" Value="X2"></asp:TreeNode>
                                        <asp:TreeNode Text="X3 环境保护管理" Value="X3"></asp:TreeNode>
                                        <asp:TreeNode Text="X4 灾害及其防治" Value="X4"></asp:TreeNode>
                                        <asp:TreeNode Text="X5 环境污染及其防治" Value="X5"></asp:TreeNode>
                                        <asp:TreeNode Text="X7 废物处理与综合利用" Value="X7"></asp:TreeNode>
                                        <asp:TreeNode Text="X8 环境质量评价与环境监测" Value="X8"></asp:TreeNode>
                                        <asp:TreeNode Text="X9 安全科学" Value="X9"></asp:TreeNode>
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="Z 综合性图书" Value="Z">
                                        <asp:TreeNode Text="Z1 丛书" Value="Z1"></asp:TreeNode>
                                        <asp:TreeNode Text="Z2 百科全书,类书" Value="Z2"></asp:TreeNode>
                                        <asp:TreeNode Text="Z3 辞典" Value="Z3"></asp:TreeNode>
                                        <asp:TreeNode Text="Z4 论文集,全集,选集,杂著" Value="Z4"></asp:TreeNode>
                                        <asp:TreeNode Text="Z5 年鉴,年刊" Value="Z5"></asp:TreeNode>
                                        <asp:TreeNode Text="Z6 期刊,连续性出版物" Value="Z6"></asp:TreeNode>
                                        <asp:TreeNode Text="Z8 图书目录,文摘,索引" Value="Z8"></asp:TreeNode>
                                    </asp:TreeNode>
                                </Nodes>
                            </asp:TreeView>
                        </div>

                    </td>
                    <td width="80%" style="vertical-align: top; height: 800px;">
                        <asp:EntityDataSource ID="edsBook" runat="server" ConnectionString="name=CDManagerDevEntities" OrderBy="it.ISBN"
                            DefaultContainerName="CDManagerDevEntities" EnableFlattening="False" EntitySetName="Book">
                            <WhereParameters>
                                <asp:SessionParameter Name="FLTM" SessionField="FLTM" DbType="String" />
                            </WhereParameters>
                        </asp:EntityDataSource>
                        <h4>当前分类:<asp:Label ID="lblFL" runat="server" ForeColor="Maroon"></asp:Label></h4>
                        <hr />
                        <center>
                            <asp:ListView ID="lvBook" runat="server" DataKeyNames="BookID" DataSourceID="edsBook" OnItemDataBound="lvBook_ItemDataBound">
                                <AlternatingItemTemplate>
                                    <tr class="alternating">
                                        <td>
                                            <asp:Label ID="ISBNLabel" runat="server" Text='<%# Eval("ISBN") %>' />
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/BookDetail.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label ID="CBSLabel" runat="server" Text='<%# Eval("CBS") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="ZRZLabel" runat="server" Text='<%# Eval("ZRZ") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="FLTMLabel" runat="server" Text='<%# Eval("FLTM") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDownloadApply" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIsOnline" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="FFSJLabel" runat="server" Text='<%# Eval("FFSJ") %>' />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                                <EmptyDataTemplate>
                                    <table id="Table1" runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #5381AC; border-style: solid; border-width: 1px;">
                                        <tr>
                                            <td>没有当前分类的图书</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <ItemTemplate>
                                    <tr class="item">
                                        <td>
                                            <asp:Label ID="ISBNLabel" runat="server" Text='<%# Eval("ISBN") %>' />
                                        </td>
                                        <td>
                                            <asp:HyperLink ID="linkZTM" runat="server" NavigateUrl='<%# Eval("ISBN","~/BookDetail.aspx?ISBN={0}") %>' Text='<%# Eval("ZTM") %>'></asp:HyperLink>
                                        </td>
                                        <td>
                                            <asp:Label ID="CBSLabel" runat="server" Text='<%# Eval("CBS") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="ZRZLabel" runat="server" Text='<%# Eval("ZRZ") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="FLTMLabel" runat="server" Text='<%# Eval("FLTM") %>' />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDownloadApply" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIsOnline" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="FFSJLabel" runat="server" Text='<%# Eval("FFSJ") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <LayoutTemplate>
                                    <table id="Table2" runat="server" style="border: 1px solid #5381AC; border-collapse: collapse" cellpadding="0">
                                        <tr id="Tr1" runat="server">
                                            <td id="Td1" runat="server">
                                                <table id="itemPlaceholderContainer" runat="server" border="1" style="border-collapse: collapse; border-color: #999999; border-style: none;">
                                                    <tr id="Tr2" runat="server" class="tableheader">
                                                        <th id="Th1" runat="server">
                                                            <asp:LinkButton ID="headerISBN" CommandArgument="ISBN" CommandName="Sort" runat="server" ForeColor="White">ISBN</asp:LinkButton></th>
                                                        <th id="Th2" runat="server">
                                                            <asp:LinkButton ID="headerZTM" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">图书名称</asp:LinkButton></th>
                                                        <th id="Th3" runat="server">出版社</th>
                                                        <th id="Th4" runat="server">作者</th>
                                                        <th id="Th5" runat="server">分类</th>
                                                        <th id="Th6" runat="server">下载数/申请数</th>
                                                        <th id="Th7" runat="server">光盘资源状态</th>
                                                        <th id="Th8" runat="server">
                                                            <asp:LinkButton ID="headerFFSJ" CommandArgument="ZTM" CommandName="Sort" runat="server" ForeColor="White">光盘操作时间</asp:LinkButton></th>
                                                    </tr>
                                                    <tr id="itemPlaceholder" runat="server">
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="Tr3" runat="server">
                                            <td id="Td2" runat="server" style="text-align: center; font-family: Verdana, Arial, Helvetica, sans-serif; padding-top: 5px; padding-bottom: 5px">
                                                <asp:DataPager ID="dpBook" runat="server" PageSize="25">
                                                    <Fields>
                                                        <asp:TemplatePagerField>
                                                            <PagerTemplate>
                                                                当前
                                            <asp:Label ID="lblPage" runat="server" ForeColor="Red" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>"></asp:Label>
                                                                /<asp:Label ID="lblPageCount" runat="server" Text="<%# Container.TotalRowCount<Container.PageSize?1:Container.TotalRowCount/Container.PageSize+1%>"></asp:Label>
                                                                页
                                                            </PagerTemplate>
                                                        </asp:TemplatePagerField>
                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                            ShowPreviousPageButton="False" />
                                                        <asp:NumericPagerField PreviousPageText="上十页" ButtonCount="10" NextPageText="下十页" />
                                                        <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                                            ShowPreviousPageButton="False" />
                                                        <asp:TemplatePagerField>
                                                            <PagerTemplate>
                                                                跳转到第<asp:TextBox ID="txtPage" runat="server" Width="20px" Text="<%# (Container.StartRowIndex/Container.PageSize)+1%>" onkeypress="if (event.keyCode < 48 || event.keyCode >57) event.returnValue = false;" />页<asp:Button ID="btnGo" runat="server" Text="GO" OnCommand="btnGo_Command" />
                                                            </PagerTemplate>
                                                        </asp:TemplatePagerField>
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>
                                    </table>
                                </LayoutTemplate>
                            </asp:ListView>
                        </center>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
