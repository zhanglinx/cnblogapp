我觉得写一个readme.me 真的很重要，不仅仅在于自己能看的懂，更重要的是当其他看这个readme的时候，能对代码结构有一个清晰的了解。
1.为什么要写？
因为我看到我两年前写的代码，我想打死我自己，各种奇怪、不规范、甚至带有误导性的命名。
2.必须要写？
没有规矩不成方圆，没有一定的规范约束，几个月后再回看这些代码，不禁发出这样的疑问？这代码是哪个sb写？在开始写博客园的xamarin android，首先我起草了这份readme。
3.android 开发命名的原则是什么？
命名要简洁明了，望问知义；命名规范定义太多、过于严格，反而让人心烦，反而没人遵守--《APP研发录》
4.camel命名法：俗称小驼峰命名法，除了首个单词首字母小写除外，其余所有单词首字母都要大写
5.pascal命名法：俗称大驼峰命名法，所有单词首字母都要大写
6.包名一律小写：【com】.【公司名/组织名】.【项目名称】.【模块名】 没办法之前18年初时提交的app到应用商店包名取的太随意，没有遵守这个规范，当然这个规范肯定不是强制必须的。
7.文件夹的功能命名规范
com.xxx.xxx.view –> 自定义view 或者是View接口
com.xxx.xxx.activities –> activity类
com.xxx.xxx.fragments –> fragment类
com.xxx.xxx.adapter –> 适配器相关
com.xxx.xxx.utils –> 公共工具类
com.xxx.xxx.bean –> 实体类
com.xxx.xxx.service –> service服务
com.xxx.xxx.broadcast –> 广播接收器
com.xxx.xxx.db –> 数据库操作类
com.xxx.xxx.persenter –> 中间对象
com.xxx.xxx.model –> 数据处理类
8. 类、方法、变量、文件的命名规范
8.1类的命名采用大驼峰命名法
Activity –> xxxActivity.java
Application –> xxxApplication.java
Fragment –> xxxFragment.java
Service –> xxxService.java
BroadcastReceiver –> xxxBroReceiver.java
ContentProvider –> xxxProvider.java
Adapter –> xxxAdapter.java
Handler –> xxxHandler.java
接口 –> xxxInter.java
接口实现类 –> xxxImpl.java
Persenter –> xxxPersenter.java
公共父类 –> BaseActivity.java、BaseFragment.java、- BaseAdapter.java等
util类 –> LogUtil.java
数据库类 –> BaseSQLiteDBHelper.java
8.2 变量的命名规范     
普通变量：camel命名法；
控件变量：我个人还是比较喜欢【控件缩写】+下划线+【控件逻辑名称的】，有的人比较习惯反过来，比如btn_login，感觉前者的可读性比较强
控件名称           控件ID            代码中控件变量
LinearLayout -> llayout_xxx    ->      llyout_xxx\llyoutXxx\xxxLlyout
TextView     -> tv_xxx         ->      tv_xxx\tvXxx\xxxTv              
Button        ->btn_xxx        ->      btn_xxx\btnXxx\xxxBtn
SwipwRefreshLayout ->srl_xxx   ->      srl_xxx\srlXxx\xxxSrl
RadioButton   ->rbtn_xxx       ->      rbtn_xxx\.....
ProgressBar   ->pbar_xxx       ->      pbar_xxx
EditText      ->et_xxx         ->      et_xxx
WebView       ->wv_xxx         ->      wv_xxx
常量命名：每个单词之间用"_"分割，全部采用大写
8.3 文件的命名
Layout下的命名：采用全部小写
activity_login.axml
fragment_index.axml
item_index.axml
Drawable目录下的命名：全部单词小写，每个单词
尽量使用png格式的图片，同样图片png体积比jpg大，但是加载速度快，app图片优先使用png格式
考虑到流量和apk体积可以使用jpg
图标：ic_read.png
背景图：bg_login.png bg_splash
selector：selector_login_btn.xml
shape：shape_login_edit.xml
layer-list:layerlist_progress_bar.xml


8.4方法的命名
采用大驼峰命名
OnCreate()、RefreshRecyclerView()、LoadWebView()

9.编码规范
代码中不要出现中文（注释除外），布局中的文字通过strings.xml 应用用来显示中文
控件声明放在Activity级别，这样在当前Activity都可以访问，相对在OnCreate中声明变量不够灵活
界面上传值尽量使用intent方式，少用全局变量
有关margin和padding的值也都放在dimens.xml 中
View的单击事件最好是通过实现接口View.IOnClickListener的方法OnClick，所有的单击事件这样看上去会比较集中
数据类型转换一定要校验






