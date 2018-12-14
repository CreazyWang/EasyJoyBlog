$(function () {
    // 读取body data-type 判断是哪个页面然后执行相应页面方法，方法在下面。
    var dataType = $('body').attr('data-type');
    for (key in pageData) {
        if (key == dataType) {
            pageData[key]();
        }
    }
})
// ===============================================
// 此处填写公共方法
//------------------------------------------------ 
//textarea限制字数
function keyUP(t) {
    var len = $(t).val().length;
    if (len > 139) {
        $(t).val($(t).val().substring(0, 140));
    }
}
function is_empty(str) {
    if (str == null || str == "" || str == undefined) {
        return true;
    } else {
        return false;
    }
}
// 页面数据
var pageData = {
    // ===============================================
    // 首页页面
    // ===============================================
    'Home': function indexData() {
        var banner = $('#banner').unslider({
            dots: true

        }), data04 = banner.data('unslider');

        $('.unslider-arrow04').click(function () {
            var fn = this.className.split(' ')[1];
            data04[fn]();
        });
    },
    'Message': function messageData() {
        //分页加载留言数据 
        $(".pageBox").pageFun({
            interFace: "/Message/CommentsList",  /*接口*/
            displayCount: 5,  /*每页显示总条数*/
            maxPage: 5,/*每次最多加载多少页*/
            dataFun: function (val) {
                
                var Html = '';
                var HfHtml = '';
                if (val.commentHfs.length > 0) {
                    $.each(val.commentHfs, function (i, val) {
                        HfHtml += '<div class="all-pl-con" data-id="' + val.id + '"><div class="pl-text hfpl-text clearfix"><a href="#" class="comment-size-name">' + val.atName + '</a><span class="my-pl-con">回复<a href="#" class="atName">@' + val.hfName + '</a> : ' + val.hfContent + '</span></div><div class="date-dz"><span class="date-dz-left pull-left comment-time">' + val.hfTime + '</span><div class="date-dz-right pull-right comment-pl-block"><a data-id="' + val.id + '" href="javascript:;" class="date-dz-pl pl-hf hf-con-block pull-left">回复</a></div></div></div>';
                    });
                }
                Html = '<div class="comment-show-con clearfix" data-id="' + val.id + '"><div class="comment-show-con-img pull-left"><img src="../images/head.jpg" width="50" alt=""></div> <div class="comment-show-con-list pull-left clearfix"><div class="pl-text clearfix"> <a href="#" class="comment-size-name">' + val.commentName + ' : </a> <span class="my-pl-con"> ' + val.commentContent + '</span> </div> <div class="date-dz"> <span class="date-dz-left pull-left comment-time">' + val.commentTime + '</span> <div class="date-dz-right pull-right comment-pl-block"><a  data-id="' + val.id + '"  href="javascript:;" class="date-dz-pl pl-hf hf-con-block pull-left">回复</a> </div> </div><div class="hf-list-con">' + HfHtml + '</div></div> </div>';
                return Html;
                //$('.comment-show').prepend(Html);

            },
            pageFun: function (i) {
                var pageHtml = '<li class="pageNum">' + i + '</li>';
                return pageHtml;
            }
        })
        $('.content').flexText();
        //点击评论创建评论条 
        $('.commentAll').on('click', '.plBtn', function () {
            var author = $("#author").val();
            var author_email = $("#author_email").val();
            if (is_empty(author)) {
                layer.msg("请输入您的昵称哦~");
                $("#username").css("border", "1px solid #ff154").css("box-shadow", "0 0 8px rgba(255, 21, 21, 0.6) inset, 0 1px 1px #fff");
                return false;
            }
            if (is_empty(author_email)) {
                layer.msg("请输入您的邮箱哦~");
                $("#username").css("border", "1px solid #ff154").css("box-shadow", "0 0 8px rgba(255, 21, 21, 0.6) inset, 0 1px 1px #fff");
                return false;
            }

            var myDate = new Date();
            //获取当前年
            var year = myDate.getFullYear();
            //获取当前月
            var month = myDate.getMonth() + 1;
            //获取当前日
            var date = myDate.getDate();
            var h = myDate.getHours();       //获取当前小时数(0-23)
            var m = myDate.getMinutes();     //获取当前分钟数(0-59)
            if (m < 10) m = '0' + m;
            var s = myDate.getSeconds();
            if (s < 10) s = '0' + s;
            var now = year + '-' + month + "-" + date + " " + h + ':' + m + ":" + s;
            //获取输入内容
            var oSize = $(this).siblings('.flex-text-wrap').find('.comment-input').val();
            if (is_empty(oSize)) {
                layer.msg("请输入您的留言内容哦~");
                $(this).siblings('.flex-text-wrap').find('.comment-input').css("border", "1px solid #ff154").css("box-shadow", "0 0 8px rgba(255, 21, 21, 0.6) inset, 0 1px 1px #fff");
                return false;
            }
            var $this = $(this);
            //发送留言
            $.ajax({
                type: "POST",
                url: "/Message/CommentsPost/",
                data: {
                    author: author,
                    author_email: author_email,
                    content: oSize,
                    parent: 0
                },
                datetype: "Json",
                success: function (message) {
                    if (message.type == "success") {
                        //动态创建评论模块
                        var oHtml = '<div class="comment-show-con clearfix" data-id="' + message.content + '"><div class="comment-show-con-img pull-left"><img src="../images/head.jpg" width="50" alt=""></div> <div class="comment-show-con-list pull-left clearfix"><div class="pl-text clearfix"> <a href="#" class="comment-size-name">' + author + ' : </a> <span class="my-pl-con">&nbsp;' + oSize + '</span> </div> <div class="date-dz"> <span class="date-dz-left pull-left comment-time">' + now + '</span> <div class="date-dz-right pull-right comment-pl-block"><a href="javascript:;" class="date-dz-pl pl-hf hf-con-block pull-left">回复</a> </div> </div><div class="hf-list-con"></div></div> </div>';
                        if (oSize.replace(/(^\s*)|(\s*$)/g, "") != '') {
                            $this.parents('.reviewArea ').siblings('.comment-show').prepend(oHtml);
                            $this.siblings('.flex-text-wrap').find('.comment-input').prop('value', '').siblings('pre').find('span').text('');
                        }
                        $(this).siblings('.flex-text-wrap').find('.comment-input').val("");
                    } else {
                        layer.msg(message.content, { icon: 2 });
                    }
                }
            });

        });
        //点击回复动态创建回复块  
        $('.comment-show').on('click', '.pl-hf', function () {
            //获取回复人的名字
            var fhName = $(this).parents('.date-dz-right').parents('.date-dz').siblings('.pl-text').find('.comment-size-name').html();
            //回复@
            var fhN = '回复@' + fhName;
            //var oInput = $(this).parents('.date-dz-right').parents('.date-dz').siblings('.hf-con');
            var fhHtml = '<div class="hf-con pull-left"> <textarea class="content comment-input hf-input" placeholder="" onkeyup="keyUP(this)"></textarea> <a href="javascript:;" class="hf-pl">评论</a></div>';
            //显示回复
            if ($(this).is('.hf-con-block')) {
                $(this).parents('.date-dz-right').parents('.date-dz').append(fhHtml);
                $(this).removeClass('hf-con-block');
                $('.content').flexText();
                $(this).parents('.date-dz-right').siblings('.hf-con').find('.pre').css('padding', '6px 15px');
                //console.log($(this).parents('.date-dz-right').siblings('.hf-con').find('.pre'))
                //input框自动聚焦
                $(this).parents('.date-dz-right').siblings('.hf-con').find('.hf-input').val('').focus().val(fhN);
            } else {
                $(this).addClass('hf-con-block');
                $(this).parents('.date-dz-right').siblings('.hf-con').remove();
            }
        });
        //评论回复块创建  
        $('.comment-show').on('click', '.hf-pl', function () {
            var author = $("#author").val();
            var author_email = $("#author_email").val();
            if (is_empty(author)) {
                layer.msg("请输入您的昵称哦~");
                $("#author").css("border", "1px solid #ff154").css("box-shadow", "0 0 8px rgba(255, 21, 21, 0.6) inset, 0 1px 1px #fff");
                return false;
            }
            if (is_empty(author_email)) {
                layer.msg("请输入您的邮箱哦~");
                $("#author_email").css("border", "1px solid #ff154").css("box-shadow", "0 0 8px rgba(255, 21, 21, 0.6) inset, 0 1px 1px #fff");
                return false;
            }
            var oThis = $(this);
            var myDate = new Date();
            //获取当前年
            var year = myDate.getFullYear();
            //获取当前月
            var month = myDate.getMonth() + 1;
            //获取当前日
            var date = myDate.getDate();
            var h = myDate.getHours();       //获取当前小时数(0-23)
            var m = myDate.getMinutes();     //获取当前分钟数(0-59)
            if (m < 10) m = '0' + m;
            var s = myDate.getSeconds();
            if (s < 10) s = '0' + s;
            var now = year + '-' + month + "-" + date + " " + h + ':' + m + ":" + s;
            //获取输入内容
            var oHfVal = $(this).siblings('.flex-text-wrap').find('.hf-input').val();

            if (is_empty(oHfVal)) {
                layer.msg("请输入您的留言内容哦~");
                $(this).siblings('.flex-text-wrap').find('.hf-input').css("border", "1px solid #ff154").css("box-shadow", "0 0 8px rgba(255, 21, 21, 0.6) inset, 0 1px 1px #fff");
                return false;
            }
            var oHfName = $(this).parents('.hf-con').parents('.date-dz').siblings('.pl-text').find('.comment-size-name').html();
            var oAllVal = '回复@' + oHfName;
            if (oHfVal.replace(/^ +| +$/g, '') == '' || oHfVal == oAllVal) {

            } else {
                var parent = $(this).parents('.hf-con').parents('.date-dz').parents('.comment-show-con-list').parent('.comment-show-con').data('id');
                //发送留言
                $.ajax({
                    type: "POST",
                    url: "/Message/CommentsPost/",
                    data: {
                        author: author,
                        author_email: author_email,
                        content: oHfVal,
                        parent: parent
                    },
                    datetype: "Json",
                    success: function (message) {
                        if (message.type == "success") {
                            //动态创建评论模块
                            var oAt = '<a href="#" class="atName">@' + author + '</a> : ' + oHfVal;
                            var oHtml = '<div class="all-pl-con"><div class="pl-text hfpl-text clearfix"><a href="#" class="comment-size-name"> </a><span class="my-pl-con">' + oAt + '</span></div><div class="date-dz"> <span class="date-dz-left pull-left comment-time">' + now + '</span> <div class="date-dz-right pull-right comment-pl-block"> <a href="javascript:;" class="date-dz-pl pl-hf hf-con-block pull-left">回复</a> </div></div>';
                            oThis.parents('.hf-con').parents('.comment-show-con-list').find('.hf-list-con').css('display', 'block').prepend(oHtml) && oThis.parents('.hf-con').siblings('.date-dz-right').find('.pl-hf').addClass('hf-con-block') && oThis.parents('.hf-con').remove();

                        } else {
                            layer.msg(message.content, { icon: 2 });
                        }
                    }
                });
            }
        });
    },
    'ArticleList': function articlelistData() {
        //博文类别点击事件
        $(".blogtype").on("click", function () {
            //获取slug
            var slug = $(this).data("slug");
            //清空元素
            $(".pageDiv").html("");
            $(".pageObj").html("");
            $(".pageBox").pageFun({
                interFace: "/ArticleList/BlogsList?type=" + slug,  /*接口*/
                displayCount: 10,  /*每页显示总条数*/
                maxPage: 5,/*每次最多加载多少页*/
                dataFun: function (val) {
                    var Tags = '';
                    $.each(val.className, function (i, v) { 
                        Tags += '<a href="/ArticleList/Index?type='+v+'" title="' + v + '" target="_blank" class="classname">' + i + '</a>&nbsp;';
                    });
                    
                    var Html = '<li><span class="blogpic"><a href="/"><img src="../images/text02.jpg"></a></span><h3 class="blogtitle"><a href="/ArticleList/Detail?id=' + val.link + '">' + val.title + '</a></h3><div class="bloginfo"><p>' + val.blogInfo + '...</p></div><div class="autor"><span class="lm">' + Tags + '</span><span class="dtime">' + val.dTime + '</span><span class="viewnum">浏览（<a href="/">0</a>）</span><span class="readmore"><a href="/ArticleList/Detail?id=' + val.link + '">阅读原文</a></span></div></li>';
                    return Html; 
                },
                pageFun: function (i) {
                    var pageHtml = '<li class="pageNum">' + i + '</li>';
                    return pageHtml;
                }
            }) 
        });
        var blogtype = $('#blogtype').val();
        //分页加载文章数据 
        $(".pageBox").pageFun({
            interFace: '/ArticleList/BlogsList?type=' + blogtype + '',  /*接口*/
            displayCount: 10,  /*每页显示总条数*/
            maxPage: 5,/*每次最多加载多少页*/
            dataFun: function (val) {
                var Tags = '';
                $.each(val.className, function (i, v) {
                    Tags += '<a href="/" title="' + v + '" target="_blank" class="classname">' + i + '</a>&nbsp;';
                });

                var Html = '<li><span class="blogpic"><a href="/"><img src="../images/text02.jpg"></a></span><h3 class="blogtitle"><a href="/ArticleList/Detail?id=' + val.link + '">' + val.title + '</a></h3><div class="bloginfo"><p>' + val.blogInfo + '...</p></div><div class="autor"><span class="lm">' + Tags + '</span><span class="dtime">' + val.dTime + '</span><span class="viewnum">浏览（<a href="/">0</a>）</span><span class="readmore"><a href="/ArticleList/Detail?id=' + val.link + '">阅读原文</a></span></div></li>';
                return Html;
            },
            pageFun: function (i) {
                var pageHtml = '<li class="pageNum">' + i + '</li>';
                return pageHtml;
            }
        })
    },
    'Detail': function articlelistData() {
        //博文类别点击事件
        $(".blogtype").on("click", function () {
            //获取slug
            var slug = $(this).data("slug");
            window.location = '/ArticleList/Index?type=' + slug + '';
        });
     
    }

}