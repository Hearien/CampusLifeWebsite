<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Hashtable>" %>
<script type="text/javascript">
    $(function () {
        $("#mytab a").click(function (e) {
            e.preventDefault();
            $(this).tab("show");
        });
    });
</script>
<style>
.topnews ul{text-align: right;
    display: flex;
    background-color: #CCCCCC;
    margin:15px 0;}
 .topnews #economy{margin-top:0;}
 .topnews ul span.toptitle{background-color: #007354;
    color: #fff;
    line-height: 40px;
    text-indent: 15px;
    padding-right:15px;
    margin:0;}
   .topnews ul .more{font-size: 12px;
    font-weight: bold;
    text-align: right;
    line-height:40px;
    color:#428bca;}
  .topnews .page-header{height:auto;padding-bottom:0;}
  .topnews .page-header a h3{color:#5d5e5e;}
</style>
<div class="col-md-12 topnews">
        <ul class="nav nav-tabs" id="mytab" role="tablist">
            
           <span class="toptitle text-center">新闻精选</span>
            
            <li class="active">
                <a href="#policy" role="tab">政治</a>
            </li>
            <li>
                <a href="#economy" role="tab">经济</a>
            </li>
            <li>
                <a href="#education" role="tab">教育</a>
            </li>
            <li>
                <a href="#sport" role="tab">体育</a>
            </li>
            <li>
                <a href="#society" role="tab">社会</a>
            </li>
            <li>
                <a href="#science" role="tab">科技</a>
            </li>
            
                <a class="pull-right more">MORE+</a>
            
        </ul>
    <div class="col-md-12">
    <%
                    var newsGov = (Model.News)Model["gov"];
                    var newsEco = (Model.News)Model["eco"];
                    var newsEdu = (Model.News)Model["edu"];
                    var newsSpo = (Model.News)Model["spo"];
                    var newsSoc = (Model.News)Model["soc"];
                    var newsSci = (Model.News)Model["sci"];
                 %>
        <div class="tab-content">
            <div class="row col-md-12 tab-pane active" id="policy">
                <div class="col-md-4">
                    <a class="thumnail">
                        <img class="img img-responsive" src="../../img/study3.png" width=160 />
                    </a>
                </div>
                <div class="col-md-8">
                    <div class="page-header">
                        <a href="/News/Details?newsid=<%=Html.Encode(newsGov.newsid) %>">
                            <h3><%=Html.Encode(newsGov.newstitle)%></h3>
                        </a>
                    </div>
                    <p><%=MvcHtmlString.Create(newsGov.newsdetsc.Replace("&lt;", "<").Replace("&gt;", ">").Replace("&middot;", "·").Substring(0, 140))%></p>
                </div>
            </div>
            <div class="row col-md-12 tab-pane" id="economy">
                <div class="col-md-4">
                    <a class="thumnail">
                        <img class="img img-responsive" src="../../img/study3.png" width="160" />
                    </a>
                </div>
                <div class="col-md-8">
                    <div class="page-header">
                        <a href="/News/Details?newsid=<%=Html.Encode(newsEco.newsid) %>">
                            <h3><%=MvcHtmlString.Create(newsEco.newstitle)%></h3>
                        </a>
                    </div>
                    <p><%=MvcHtmlString.Create(newsEco.newsdetsc.Replace("&lt;", "<").Replace("&gt;", ">").Substring(0, 140))%></p>
                </div>
            </div>
            <div class="row col-md-12 tab-pane" id="education">
                <div class="col-md-4">
                    <a class="thumnail">
                        <img class="img img-responsive" src="../../img/study3.png" width="160" />
                    </a>
                </div>
                <div class="col-md-8">
                    <div class="page-header">
                        <a href="/News/Details?newsid=<%=Html.Encode(newsEdu.newsid) %>">
                            <h3><%=MvcHtmlString.Create(newsEdu.newstitle)%></h3>
                        </a>
                    </div>
                    <p><%=MvcHtmlString.Create(newsEdu.newsdetsc.Replace("&lt;", "<").Replace("&gt;", ">").Substring(0, 140))%></p>
                </div>
            </div>
            <div class="row col-md-12 tab-pane" id="sport">
                <div class="col-md-4">
                    <a class="thumnail">
                        <img class="img img-responsive" src="../../img/study3.png" width=160 />
                    </a>
                </div>
                <div class="col-md-8">
                    <div class="page-header">
                        <a href="/News/Details?newsid=<%=Html.Encode(newsSpo.newsid) %>">
                            <h3><%=Html.Encode(newsSpo.newstitle)%></h3>
                        </a>
                    </div>
                     <p><%=MvcHtmlString.Create(newsSpo.newsdetsc.Replace("&lt;", "<").Replace("&gt;", ">").Substring(0, 140))%></p>
                </div>
            </div>
            <div class="row col-md-12 tab-pane" id="society">
                <div class="col-md-4">
                    <a class="thumnail">
                        <img class="img img-responsive" src="../../img/study3.png" width="160" />
                    </a>
                </div>
                <div class="col-md-8">
                    <div class="page-header">
                        <a href="/News/Details?newsid=<%=Html.Encode(newsSoc.newsid) %>">
                            <h3><%=Html.Encode(newsSoc.newstitle)%></h3>
                        </a>
                    </div>
                    <p><%=MvcHtmlString.Create(newsSoc.newsdetsc.Replace("&lt;", "<").Replace("&gt;", ">").Substring(0, 140))%></p>
                </div>
            </div>
            <div class="row col-md-12 tab-pane" id="science">
                <div class="col-md-4">
                    <a class="thumnail">
                        <img class="img img-responsive" src="../../img/study3.png" width="160" />
                    </a>
                </div>
                <div class="col-md-8">
                    <div class="page-header">
                        <a href="/News/Details?newsid=<%=Html.Encode(newsSci.newsid) %>">
                            <h3><%=Html.Encode(newsSci.newstitle)%></h3>
                        </a>
                    </div>
                    <p><%=MvcHtmlString.Create(newsSci.newsdetsc.Replace("&lt;", "<").Replace("&gt;", ">").Substring(0, 140))%></p>
                </div>
            </div>
        </div>  
    </div>
</div>