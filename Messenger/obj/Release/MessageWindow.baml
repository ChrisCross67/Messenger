   M S B A M L   `   `   `  ���� G  CMessenger, Version=1.1.300.49, Culture=neutral, PublicKeyToken=null;"clr-namespace:Messenger.ConvertersMessenger.Converters  U QMaterialDesignThemes.Wpf, Version=1.1.0.234, Culture=neutral, PublicKeyToken=null{Sclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf#MaterialDesignThemes.Wpf.Converters %clr-namespace:Messenger	Messenger  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 1clr-namespace:Messenger.UtilsMessenger.Utils  eHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.WpfMaterialDesignThemes.Wpf     Messenger.MessageWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     T PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;controls,http://metro.mahapps.com/winfx/xaml/controls  2	converter"clr-namespace:Messenger.Converters  d
convertersSclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf  #localclr-namespace:Messenger  0sys&clr-namespace:System;assembly=mscorlib  )utilsclr-namespace:Messenger.Utils  RwpfHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.Wpf  -   $��messageWindow��$	��500��$	��400��  � AllowsTransparency  . $X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�   AccentColorBrush(  "MahApps.Metro.Controls.MetroWindow   	GlowBrush# �    � Icon$1 +/Messenger;component/Icons/User-Message.png��   WindowTransitionsEnabled .   � Title �� *Member.UserName+ � 	Resources   BooleanToVisibility/   )Messenger.Converters.ToCheckableConverter  ToCheckable(   "Messenger.Converters.IconConverter  IconConverter,   &Messenger.Converters.FileIconConverter  FileIconConverter,   &Messenger.Converters.FileSizeConverter  FileSizeConverter  MenuItemAttach �� Type  Locator �� Source �� Path	 �� 
TargetType
 �� BasedOn �� Property �� Value �� Triggers �� Binding �� Value  AttachmentItem �� 	Converter!   Messenger.Utils.ImageButton  Name!   Messenger.Utils.ImageButton   Click   Image � ToolTip �� Padding 	 	TextBrush 
 SendAttachmentItem �� Orientation  ReceiveAttachmentItem d� 	IsChecked  AttachmentPanelStyle  SenderTemplate �� ElementName �� Mode �� UpdateSourceTrigger  MaterialDesignDivider �� CornerRadius � RowDefinitions � ColumnDefinitions  PrimaryHueLightBrush �� Padding " PrimaryHueLightForegroundBrush �� StringFormat  �� 
IsReadOnly! �� TextWrapping" � ContextMenu# m� Command$ � DataContext
% � Tag& �� RelativeSource' �� AncestorType( J� OpacityMask) j� Stretch* !� Visual+ J� RenderTransformOrigin, �� Stretch- �� Angle. �� ScaleX/ �� ScaleY  SelfTemplate $  SelfMessageMaterialDesignTextBox��%5  &	       &	      &	 
     &	      &	      &	      7  7  7  7m�&	 �    7  7  &	
 �    7  &	 �    &	     &	 j    7  7  7  7  7  7  &	 �    7  7  7  79�7 
 7  ��         �� " m�.s�m� -   ���� 8   $/ )LocalizationHelper.[Resources.AttachFile]�m� -   ���� 8  $2 ,LocalizationHelper.[Resources.AttachFolders]�m� -   ���� 8  $- 'LocalizationHelper.[Resources.ClearAll]����� "	 m�8
  .���  � y�$ Visible=� ��  �� $ Attachments.Count� 0.a���  � y�$ 	Collapsed=��� .R�]� ��. .+��� $��16��$��16��$��0,0,4,0q�$
��Left=�$��UniformToFill=����� $ FullPath�8     $ buttonRemove�� buttonRemove_Click$��32��$��23��$��Right=�$+ %/Messenger;component/Icons/remove.png�� �� 8  $3 -LocalizationHelper.[Resources.RemoveFromList]��� $��23��$��Stretch=�$ 0q�#��� 	 ���� *Name+�� .R��� $ 
Horizontal=�.+��� $��16��$��16��$��0,0,4,0q�$
��Left=�$��UniformToFill=����� $ Item.FullPath�8   �� #��� 	 $��Center=����� *	Item.Name+�� .R��� $ 
Horizontal=�.+���  �� *	Checked+�� #��� 	 $��Center=����� *	Item.Name+�� "	 K� ��  �� *HasAttachment+	 False.a���  � y�$ 	Collapsed=��� .R��� $	��300��$	��0,2q�$
��Left=�$
��Auto=����� $ messageWindow��$ ActualWidth�$ OneWay=�$ PropertyChanged=�.���� #���  $ 8��._��  �� �� $
��Auto � �� $
��Auto ��� $��* �.+��� $��2��#���  $ 2 0 0 2��$ 8q�#���  ._��� ���� $
 Date�$ {0:d}
{0:T}���� $��1��$��8q�$
��Left=�
������ $��0q�#��� 	   . $
! Wrap=����� *	Content+.��" �� .@�m� $# ApplicationCommands.Copy������ 8   $) #LocalizationHelper.[Resources.Copy]�.��9� $��1��$��1������ 8  $/ )LocalizationHelper.[Resources.Attachment]�y��� $ HasAttachment�$ PropertyChanged=�8  .���� $��attachmentPanel��$ 
Horizontal=�.+��� $��listBoxAttachmentList��$��28��$��90��$��0,4,0,4q�8�� $ �� $ Attachments����� $ .�8  �� -   $��Stretch=�$	��5,4q�#���   
�������% �� $ listBoxAttachmentList�� �� 8  $) #LocalizationHelper.[Resources.Save]�.���� $��15��$��15������ & �� $' ButtonL�$ 
Foreground�( !� $
) Fill=�* F� $��20��$��20�������Mx�/��S
��������@@��t�^�@@��t�^�@@�W�?�# �]@�@E�]@�@E�[C8��Sc�3��� �`P@RH^��RH^������'��i�����i������xP�|���u�ۇdV�V(c5��V(c��#dOc��?���?����|іm��|��q�H��&�z;�Lwk��>�w�Ji���x�T�H^��T�H^���i�lzѩ���l�ӹ������#���#�Ӽ'D���.>R���.>R�љ�+O�KӤ!'�( u��� u���Ӥ���0����I0����I��J��'eѵJ��Ne�� �B�h1�JN &?�2>�^��@@Mx�/��p�G(k8�p!@Q�~k8�p!@��~@45ѨG(@450��C;�@*��@
W!@Q�� *��@
W!@��� �W�C,��WQC,�*��@
W!@Q(h!*��@
W!@Q(h!{���@@��O![q���U)��	�)����)���(F�)��X���X��{tK�X����ѯ� ��ѯ� ���X�����X��z$8���C;�@z$8ppJ� ���ae��?QЧ	���ae��?QЧ	A�!  @QpJ� A�!  @�/6    ��j�    Q�j�A�!  @Q�y�A�!  @Q�y�n��bK
@Q/6n��bK
@�        ��|x;�@    �|x;�@A�!  @���  @A�!  @����  @Kٳ�    Kٳ	
���  ��$+ 0.5,0.5&�$, Uniform=�z�V� ~��� $- 0Q��� $. 1Q�$/ 1Q��� .R��� $	��300��$	��0,2q�$
��Auto=����� $ messageWindow��$ ActualWidth�$ OneWay=�$ PropertyChanged=�.���� $��Right=�#���   $ 8��._��  �� �� $
��Auto � �� $��* ��� $
��Auto �.+��� $��8q�$��Right=�
������ 
�������$��0 0 0 2q�
�������  . 8��  $
! Wrap=����� *	Content+.��" �� .@�m� $# ApplicationCommands.Copy������ 8  $) #LocalizationHelper.[Resources.Copy]�.���� $��2��$��1��#���  $ 2 0 0 2��$ 8q�#���  ._��� ���� $
 Date�$ {0:d}
{0:T}��9� $��1��$��0������ 8  $/ )LocalizationHelper.[Resources.Attachment]����� 8
  "	 9�.���  � y�$ Visible=� ��  �� $ Attachments.Count� 0.a���  � y�$ 	Collapsed=�.���� $��listBoxAttachmentList��$��28��$��90��$��0,4,0,4q�8�� $ �� $ Attachments����� $ .�8  .���  �� $��* ��� $
��Auto �.+��� $��5q�$��Stretch=�$
��Auto=�.���� -   0 ��Name$0 ItemsControlMessage��
������ $��13(���. 1 %� IsVirtualizing1 . ���� $ Messages�$ OneWay=�$ PropertyChanged=���+   %Messenger.MessageItemTemplateSelector  2   SelfTemplate#2 [ 3   SenderTemplate#3 [ � -   $��grid2��$��2�� �� $��2 ��� $
��Auto ��� $
��Auto �$	��150�� �� �� $��50 �.+��� $��0��$��1��$��2��$��Stretch=�  BlackColorBrush#���  �� $��1��$��2������ "	 ��.���  � y�$ Visible=� ��  �� $ Attachments.Count� 0.a���  � y�$ 	Collapsed=�.+�]� -   $��attachmentPanel��$��0��$��2����. $	��150��.+��� -	   $��listBoxAttachmentList��$��1,1,5,1q�4 J� 	AllowDrop4 . #��[ ���� *Attachments+�� $��1��$��2��$��1��$��Stretch=�#���  �� -
   $��2��$��Stretch=�$��Stretch=�$��Stretch=�5 �� AcceptsReturn5 . 6 �� 
AcceptsTab6 . 7 �� AutoWordSelection7 . 8 �� 	IsEnabled8 . $
! Wrap=����� $ MessageToSend�$ TwoWay=�$ PropertyChanged=�.	  (MaterialDesignThemes.Wpf.TextFieldAssist9 	  Hint9 �� # [ $2 ,LocalizationHelper.[Resources.MessageToSend]�.��" �� -     MetroContextMenu#��[ .���� $��2��$��1��.+��� $��Stretch=�#���   #���  ���� !
   Messenger.MessagerViewModel: 
  Instance# Z: $ SendCommand����� $ messageWindow��$ DataContext� �� # [ $) #LocalizationHelper.[Resources.Send]����� #
 [B��"	 ��.���  � {� .  ��  �� $ MessageToSend.Length�; �� FallbackValue$; 0�� 0.a���  � {� .  .���� $��15��$��15������ & �� $' ButtonL�$ 
Foreground�( !� $
) Fill=�* F� $��20��$��20������p    <H��Т@Q�6R <H��Т@Q�6R K֗[�!@Q    K֗[�!@p��� כ�� 0@Q�~9�k��y"@Q�K.כ�� 0@�WO�8�� �.|3 1�@8�� p    ���85�@Q�6R ���85�@Q�6R EU�k@Q    EU�k@p    3�=G?5 @Q�6R 3�=G?5 @Q�6R �`�-�O@Q    �`�-�O@p�{��#��S�?��{���� Q`�71�86�@0.|3 1�@�#��S�?Qh,� 1�86�@�.|3 1�@��� �.|3 1�@    �WO�    Q�~�5q@	
���  ��$+ 0.5,0.5&�$, Uniform=�z�V� ~��� $- 0Q��� $. 1Q�$/ 1Q�+  %MahApps.Metro.Controls.DropDownButton  -   $��DownButton1��$��Stretch=�#���  #���   ���� # [ $ .� �� # [ $+ %LocalizationHelper.[Resources.Attach]�<   Content< �� $��15��$��15������ & �� $' controls:DropDownButtonL�$ 
Foreground�( !� $
) Fill=�* F� $��20��$��20���������� �H  ��Ni#�5 X<�%X�� `��'l���^�+4 &�ۥ+0_�
��'(F8�ا!"�&��$"�k*��oxRAӠf;�2@)��g8p��聺����<�^���P*�]���%��2�ш���b��X!��Ә1���E�#��3z��N�� :�(��H��P����	����/c�s[���x�Q�Z]��@�_0�z��y\X�x���$����`_�&b�	���&���X*^$�{���\�!���`v�$�h�lEѸ��Es�a2U0*�P@I���е��)�ٝ��� �c"Ӹ�E��$�Dp���#�$���!���)�q��@��,Ps6Ѹ1���#Ӡ<�8�'����' �z
`D%��r
��%����4Q����z�GZ�@�����(������x#��U6�����S�L�H.�!�0@�S������ �H  	
���  ��$+ 0.5,0.5&�$, Uniform=�z�V� ~��� $- 0Q��� $. 1Q�$/ 1Q�