   M S B A M L   `   `   `  ����G  CMessenger, Version=1.1.298.36, Culture=neutral, PublicKeyToken=null;"clr-namespace:Messenger.ConvertersMessenger.Converters  5      U QMaterialDesignThemes.Wpf, Version=1.1.0.234, Culture=neutral, PublicKeyToken=null{Sclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf#MaterialDesignThemes.Wpf.Converters 5      %clr-namespace:Messenger	Messenger  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      1clr-namespace:Messenger.UtilsMessenger.Utils  5	      eHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.WpfMaterialDesignThemes.Wpf 5
          Messenger.MessageWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      T PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;controls,http://metro.mahapps.com/winfx/xaml/controls  5      2	converter"clr-namespace:Messenger.Converters  5      d
convertersSclr-namespace:MaterialDesignThemes.Wpf.Converters;assembly=MaterialDesignThemes.Wpf  5      #localclr-namespace:Messenger  5      0sys&clr-namespace:System;assembly=mscorlib  5      )utilsclr-namespace:Messenger.Utils  5	      RwpfHclr-namespace:MaterialDesignThemes.Wpf;assembly=MaterialDesignThemes.Wpf  5
      -   5        $��messageWindow��5      $	��500��5      $	��400��5        � AllowsTransparency  . $X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5         AccentColorBrush(  "MahApps.Metro.Controls.MetroWindow   	GlowBrush# �   5       � Icon$1 +/Messenger;component/Icons/User-Message.png��5         WindowTransitionsEnabled .   � Title 5      �� *Member.UserName+ � 	Resources 5        BooleanToVisibility/   )Messenger.Converters.ToCheckableConverter  ToCheckable(   "Messenger.Converters.IconConverter  IconConverter,   &Messenger.Converters.FileIconConverter  FileIconConverter,   &Messenger.Converters.FileSizeConverter  FileSizeConverterJ  DMaterialDesignThemes.Wpf.Converters.TextFieldHintVisibilityConverter $  TextFieldHintVisibilityConverter $  SelfMessageMaterialDesignTextBox �� 
TargetType �� Property �� Value	 �� RelativeSource
 �� AncestorType �� Path  MaterialDesignTextBoxBorder.  (MaterialDesignThemes.Wpf.TextFieldAssist-  'System.Windows.Input.KeyboardNavigation )	 %MaterialDesignValidationErrorTemplate �� 
TargetType ��Name �� Padding J� SnapsToDevicePixels ��Name ��Name J� IsHitTestVisible J� Opacity �� 	Converter(	  "MaterialDesignThemes.Wpf.Underline 	 Name �� Triggers P� Property P� Value �� 
TargetName 
 ValidationErrorBrush �� Triggers ]� 
Conditions �� Property �� Value  MenuItemAttach �� Type  Locator �� Source  AttachmentItem  �� 	Converter!
   Messenger.Utils.ImageButton! 
 Name!   Messenger.Utils.ImageButton"   Click# 
  Image$ � ToolTip% �� Padding " PrimaryHueLightForegroundBrush  SendAttachmentItem& �� Orientation  ReceiveAttachmentItem' d� 	IsChecked  AttachmentPanelStyle( �� Binding) �� Value  SenderTemplate* �� ElementName+ �� Mode, �� UpdateSourceTrigger  GrayBrush10- �� CornerRadius. � RowDefinitions/ � ColumnDefinitions  PrimaryHueLightBrush0 �� StringFormat  	TextBrush1 �� 
IsReadOnly2 �� TextWrapping3 � ContextMenu4 m� Command5 � DataContext
6 � Tag7 J� OpacityMask8 j� Stretch9 !� Visual: J� RenderTransformOrigin; �� Stretch< �� Angle= �� ScaleX> �� ScaleY  SelfTemplate? �� BasedOn��%G!  &	       &	      &	 &     &	 9     &	 L     &	 _     &	 r     7 	 7  &	 �    7  7  7  &	 
    7  7  &	 e    7  &	 �    &	 A    &	 �    7  7  7  7  7  7  &	 �    7  7  7  79�7  7  �� 5   
   6'     5   
   6)     5   
   6"     5   
   6&     5   
   6&     5   
   66   �� 5    
   " ��69   .�5!      ��  � �� 6+   �� 	 �� "
 �$ (TextElement.Foreground)��� 5"       � ��# �  6,   �� 5#       � ��$ 0 0 0 1q�60   �� 5$       �@ TextBoxViewMargin$ 1 0 1 0q�6F   �� 5%       � ��
 ���� 6+   �� 5&       �@��
CaretBrush 6+   �� 	 �� *Self+$ BorderBrush��� 5'       �@ TabNavigation$
 None=�6A   �� 5(       � ��$
 Left=�6;   �� 5)       � ��$	 Top=�69   �� 5*       �@�FocusVisualStyle 61   Z� �� 5+       �@J�	AllowDrop . 6*   �� 5,       �@��PanningMode$ VerticalFirst=�69   �� 5-       �@��IsFlicksEnabled .  67   �� 5.       �@=�ErrorTemplate8   69   �� 5/       � �� 50      �� 51      " ��6&   .R�52      � #��z��6   .+�53      �� $ border��6%   #��z��54   %   #��z��55   %   #��z��56   %   #��z��57   %   $ 0 4 0 4q�58   %    . ._�5:   "   � #��z��6'   .+�5;   &   �� $ PART_ContentHost��63   |�.  $��Hidden=�5=   3   $��Hidden=�5>   3   �� 5?   &   $
 Hint��60   $��1 0 1 0q�5@   0    .   5B   0   �� $' !(wpf:TextFieldAssist.HintOpacity)�	 �� *TemplatedParent+��5D   0   �� $  (wpf:TextFieldAssist.Hint)�	 �� *TemplatedParent+y�5F   0   �� *Text+8  5H   #   5I      	  5J      $ 	Underline��6,   5K       5L      P� 5M       � {� .  . �5N   "   �� $ border��6)    �@J�Opacity$
 0.56Q�6P   5O      P� 5P       �@J�IsMouseOver . . �5Q   "   ��  � ��
 �����6@   5R      P� 5S       �@J�IsKeyboardFocused .  . �5T   "   �� $ 	Underline��6)    �@	 IsActive . 6T   5U      P� 5V       �@=�HasError . . �5W   "   �� # � 
 6@    � ���� 5X   "   $ 	Underline��6)   # � 
 6V    � ��5Y      5[      5]       5^      ]� 5_       5`      �� 5a      + �@��#IsInactiveSelectionHighlightEnabled . 6S   �� 5b       �@��IsSelectionActive .  6A   ./�5d      ��  �@��SelectionBrush# � /�67   5e      5g      �� 5h   
   " m�6)   .s�5i      m� -   5        ��5i   2   �� 8   $/ )LocalizationHelper.[Resources.AttachFile]�m� 5j      -   5        ��5j   :   �� 8  $2 ,LocalizationHelper.[Resources.AttachFolders]�m� 5k      -   5        ��5k   3   �� 8  $- 'LocalizationHelper.[Resources.ClearAll]�5l      �� 5m   
   .R�5n      ]� ��. .+�5o      �� $��16��6   $��16��5p      $��0,0,4,0q�5q      $
��Left=�5r      $��UniformToFill=�5u      ��5s      �� $ FullPath�8    5u      
  5v      $! buttonRemove��6$   " buttonRemove_Click5y   $   $��32��5w   $   $��23��5x   $   $��Right=�5z   $   $+# %/Messenger;component/Icons/remove.png��5{   $   $ 5|   $   �� 8  $3 -LocalizationHelper.[Resources.RemoveFromList]��� 5~      $��23��6   $��Stretch=�5      $% 0q�5�      #���  5�      ��5�      �� *Name+5�      5�      5�      �� 5�   
   .R�5�      �� $& 
Horizontal=�6   .+�5�      �� $��16��6   $��16��5�      $��0,0,4,0q�5�      $
��Left=�5�      $��UniformToFill=�5�      ��5�      �� $ Item.FullPath�8    5�      �� 5�      #���  67   ��6   �� *	Item.Name+67   5�      5�      �� 5�   
   .R�5�      �� $& 
Horizontal=�6   .+�5�      �� ' 6   �� *	Checked+�� 5�      #���  67   ��6   �� *	Item.Name+67   5�      5�      �� 5�   
   " K�6-    5�      �� 5�      ( 6   �� *HasAttachment+	) False6@   .a�5�      ��  � y�$ 	Collapsed=�63   5�      5�      �� 5�   
   .R�5�      �� $	��300��5�      $	��0,2q�5�      $
��Left=�5�      $
��Auto=�5�      ��5�      �� $* messageWindow��$ ActualWidth�$+ OneWay=�$, PropertyChanged=�.��5�      �� #���  6   $- 8��6D   ._�5�      � . 5�      �� 5�      �� 5�      $
��Auto �6,   / 5�      �� 5�      $
��Auto �6/   �� 5�      $��* �6/   .+�5�      �� $��2��6!   #���  5�   !   $- 2 0 0 2��5�   !   $ 8q�5�   !   #���  5�   !   ._�5�      �� ��6(   �� $
 Date�$0 {0:d}
{0:T}��5�      �� 5�      $��1��6"   $��8q�5�   "   $
��Left=�5�   "   
������ $��0q�5�   "   #���  5�   "   1 . $
2 Wrap=�5�   "   ��5�   "   �� *	Content+.��5�   6   3 5�      �� 5�   "   .@�5�   &   m� $4 ApplicationCommands.Copy��6/   ��6R   �� 8   $) #LocalizationHelper.[Resources.Copy]�5�   #   .��5�   3   5�      9� 5�      $��1��6#   $��1��5�   #   ��5�   #   �� 8  $/ )LocalizationHelper.[Resources.Attachment]�y�5�   #   �� $ HasAttachment�$, PropertyChanged=�8   .��5�      �� $��attachmentPanel��6)   $& 
Horizontal=�6@   .+�5�   "   �� $��listBoxAttachmentList��6*   $��28��5�   *   $��90��5�   *   $��0,4,0,4q�5�   *   8�� 5 5�   *   �� $ Attachments���5�   *   �� $ .�8   �� 5�   "   -   5        $��Stretch=�5�   )   $	��5,4q�6E   #���   5�   )   
�������6 5�   U   �� $* listBoxAttachmentList��$ 5�   )   �� 8  $) #LocalizationHelper.[Resources.Save]�.��5�   &   �� $��15��60   $��15��5�   0   ��5�   0   �� 	 �� $
 ButtonL�$ 
Foreground�7 5�   *   !� 5�   .   $
8 Fill=�6:   9 5�   2   F� 5�   6   $��20��6;   $��20��5�   ;   �����Mx�/��S
��������@@��t�^�@@��t�^�@@�W�?�# �]@�@E�]@�@E�[C8��Sc�3��� �`P@RH^��RH^������'��i�����i������xP�|���u�ۇdV�V(c5��V(c��#dOc��?���?����|іm��|��q�H��&�z;�Lwk��>�w�Ji���x�T�H^��T�H^���i�lzѩ���l�ӹ������#���#�Ӽ'D���.>R���.>R�љ�+O�KӤ!'�( u��� u���Ӥ���0����I0����I��J��'eѵJ��Ne�� �B�h1�JN &?�2>�^��@@Mx�/��p�G(k8�p!@Q�~k8�p!@��~@45ѨG(@450��C;�@*��@
W!@Q�� *��@
W!@��� �W�C,��WQC,�*��@
W!@Q(h!*��@
W!@Q(h!{���@@��O![q���U)��	�)����)���(F�)��X���X��{tK�X����ѯ� ��ѯ� ���X�����X��z$8���C;�@z$8ppJ� ���ae��?QЧ	���ae��?QЧ	A�!  @QpJ� A�!  @�/6    ��j�    Q�j�A�!  @Q�y�A�!  @Q�y�n��bK
@Q/6n��bK
@�        ��|x;�@    �|x;�@A�!  @���  @A�!  @����  @Kٳ�    Kٳ	
���  ��$: 0.5,0.5&�5�   ;   $; Uniform=�5�   ;   z�5�   :   V� 5�   >   ~�5�   F   �� $< 0Q�6V   �� 5�   F   $= 1Q�6U   $> 1Q�6`   5�   ?   5�   7   5�   /   5   '   5  #   5     5     5     5     5     5     �� 5	  
   .R�5
     �� $	��300��5     $	��0,2q�5     $
��Auto=�5     ��5
     �� $* messageWindow��$ ActualWidth�$+ OneWay=�$, PropertyChanged=�.��5     �� $��Right=�6   #���   5     $- 8��5     ._�5     � . 5     �� 5     �� 5     $
��Auto �6,   / 5     �� 5     $��* �6/   �� 5     $
��Auto �6/   .+�5     �� $��8q�6"   $��Right=�5  "   
������ 
�������$��0 0 0 2q�5!  "   
�������1 . 8��  $
2 Wrap=�5&  "   ��5%  "   �� *	Content+.��5&  6   3 5'     �� 5(  "   .@�5)  &   m� $4 ApplicationCommands.Copy��6/   ��6R   �� 8  $) #LocalizationHelper.[Resources.Copy]�5*  #   .��5+  3   5,     �� 5.     $��2��6!   $��1��5/  !   #���  50  !   $- 2 0 0 2��51  !   $ 8q�52  !   #���  53  !   ._�54     �� ��6(   �� $
 Date�$0 {0:d}
{0:T}��55     9� 56     $��1��6#   $��0��57  #   ��58  #   �� 8  $/ )LocalizationHelper.[Resources.Attachment]���5:     �� 5;  "   8?  " 9�6U   .�5<  &   ��  � y�$ Visible=�6C    5=  &   �� 5>  *   ( 66   �� $ Attachments.Count�) 06a   .a�5?  .   ��  � y�$ 	Collapsed=�6K   5@  +   5B  #   .��5D     �� $��listBoxAttachmentList��6&   $��28��5E  &   $��90��5F  &   $��0,4,0,4q�5G  &   8�� 5 5H  &   �� $ Attachments���5J  &   �� $ .�8   5L     5M     5N     5O     5Q     5R     .��5S     � . 5T  
   �� 5U     $��* �6   �� 5V     $��5 �6   �� 5W     $
��Auto �6   .+�5s  
   �� $��5q�6   $��Stretch=�5t     $
��Auto=�5u     .��5v     �� -   5        @ ��Name$@ ItemsControlMessage��5v     
������ $��13(�5x     ��. A %� IsVirtualizingA . ��5y     �� $ Messages�$+ OneWay=�$, PropertyChanged=���5~     +   %Messenger.MessageItemTemplateSelector  5     B   SelfTemplate#B [ 68   C   SenderTemplate#C [ 6e   5�     5�     �� 5�  
   $��1��6   $��1��5�     $��Stretch=�5�       BlackColorBrush#���  5�     � 5�  
   -   5        $��grid2��5�     $��2��6   . 5�     �� 5�     $
��Auto �6    $	��150��6.   �� 5�     $��5 �6    �� 5�     $
��Auto �6    $	��150��6.   / 5�     �� 5�     �� 5�     $��50 �6#   .+�5�     ]� -   5        $��attachmentPanel��5�     $��0��5�     $��2��5�     ��. .+�5�     �� -	   5        $��listBoxAttachmentList��5�     $��1,1,5,1q�5�     D J� 	AllowDropD . #��[ 5�     ��5�     �� *Attachments+5�     5�     �� 5�     $��1��6   $��2��5�     $��1��5�     $��Stretch=�5�     #���  5�     �� 5�     -
   5        $��2��5�     $��Stretch=�5�     $��Stretch=�5�     $��Stretch=�5�     E �� AcceptsReturnE . F �� 
AcceptsTabF . G �� AutoWordSelectionG . H �� 	IsEnabledH . $
2 Wrap=�5�     ��5�     �� $ MessageToSend�$+ TwoWay=�$, PropertyChanged=�I   HintI 5�     �� # [ $2 ,LocalizationHelper.[Resources.MessageToSend]�.��5�  m   3 5�     �� 5�     -   5          MetroContextMenu#��[ 5�  M   .��5�  '   5�     �� 5�     $��2��6   $��1��6&   .+�5�     �� $��Stretch=�6   #���   5�     
���������5�     �� !   Messenger.MessagerViewModelJ   Instance# ZJ $ SendCommand���5�     �� $* messageWindow��$ DataContext�$ 5�     �� # [ $) #LocalizationHelper.[Resources.Send]���5�     �� 5�     #? [B��6    " ��6K   .�5�     ��  � {� . 6:    5�     �� 5�  "   ( 6.   �� $ MessageToSend.Length�K �� FallbackValue$K 0��) 06m   .a�5�  &   ��  � {� .  6B   5�  #   5�     .��5�     �� $��15��6    $��15��5�      ��5�      �� 	 �� $
 ButtonL�$ 
Foreground�7 5�     !� 5�     $
8 Fill=�6*   9 5�  "   F� 5�  &   $��20��6+   $��20��5�  +   ����p    <H��Т@Q�6R <H��Т@Q�6R K֗[�!@Q    K֗[�!@p��� כ�� 0@Q�~9�k��y"@Q�K.כ�� 0@�WO�8�� �.|3 1�@8�� p    ���85�@Q�6R ���85�@Q�6R EU�k@Q    EU�k@p    3�=G?5 @Q�6R 3�=G?5 @Q�6R �`�-�O@Q    �`�-�O@p�{��#��S�?��{���� Q`�71�86�@0.|3 1�@�#��S�?Qh,� 1�86�@�.|3 1�@��� �.|3 1�@    �WO�    Q�~�5q@	
���  ��$: 0.5,0.5&�5�  +   $; Uniform=�5�  +   z�5�  *   V� 5�  .   ~�5�  6   �� $< 0Q�6F   �� 5�  6   $= 1Q�6E   $> 1Q�6P   5�  /   5�  '   5�     5�     5�     +  %MahApps.Metro.Controls.DropDownButton  5�     -   5        $��DownButton1��5�  *   $��Stretch=�5�  *   
���������5�  *   �� # [ $ .�$ 5�  *   �� # [ $+ %LocalizationHelper.[Resources.Attach]�L   ContentL 5�     �� 5�     $��15��6$   $��15��5�  $   ��5�  $   �� 	 �� $
 controls:DropDownButtonL�$ 
Foreground�7 5�     !� 5�  "   $
8 Fill=�6.   9 5�  &   F� 5   *   $��20��6/   $��20��5  /   �������� �H  ��Ni#�5 X<�%X�� `��'l���^�+4 &�ۥ+0_�
��'(F8�ا!"�&��$"�k*��oxRAӠf;�2@)��g8p��聺����<�^���P*�]���%��2�ш���b��X!��Ә1���E�#��3z��N�� :�(��H��P����	����/c�s[���x�Q�Z]��@�_0�z��y\X�x���$����`_�&b�	���&���X*^$�{���\�!���`v�$�h�lEѸ��Es�a2U0*�P@I���е��)�ٝ��� �c"Ӹ�E��$�Dp���#�$���!���)�q��@��,Ps6Ѹ1���#Ӡ<�8�'����' �z
`D%��r
��%����4Q����z�GZ�@�����(������x#��U6�����S�L�H.�!�0@�S������ �H  	
���  ��$: 0.5,0.5&�5  /   $; Uniform=�5  /   z�5  .   V� 5  2   ~�5	  :   �� $< 0Q�6J   �� 5
  :   $= 1Q�6I   $> 1Q�6T   5  3   5  +   5  #   5     5     5     5     5     5     