   M S B A M L   `   `   `  ����G  CMessenger, Version=1.1.298.36, Culture=neutral, PublicKeyToken=null;"clr-namespace:Messenger.PropertiesMessenger.Properties  5      ;"clr-namespace:Messenger.ConvertersMessenger.Converters  5      5clr-namespace:Messenger.LocatorMessenger.Locator  5          Messenger.OptionWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      9
Properties"clr-namespace:Messenger.Properties     5      T PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;controls,http://metro.mahapps.com/winfx/xaml/controls  5      2	converter"clr-namespace:Messenger.Converters  5      -locatorclr-namespace:Messenger.Locator  5      -   5          � Title$  Options��5      $	��367��5	      $	��319��5
      $X��Rpack://application:,,,/MaterialDesignThemes.Wpf;component/Resources/Roboto/#Roboto)�5         AccentColorBrush(  "MahApps.Metro.Controls.MetroWindow   	GlowBrush# �   5      $��5q�5       � ShowInTaskbar .   � WindowStartupLocation$ CenterOwner=�5       � WindowStyle$ 
ToolWindow=�5         WindowTransitionsEnabled .   � 	Resources 5      (   "Messenger.Converters.FlagConverter 
 ToFlag,   &Messenger.Converters.FileIconConverter  FileIconConverter �� 
TargetType �� Property	 �� Value
 �� BasedOn��%9  &	       &	      '�� &     '�� �     7��  5   
   6"     5   
   6&   �� 5   
   " ��6   .�5      ��  � ��$	 14(�6)   �� 5       � ��$	 Center=�62   �� 5       � ��$
	 Bold"�6+   5      �� 5   
   8
   " ��6<   .�5      ��  � ��$	 Center=�62   5      5       .��5!      �  � ColumnDefinitions 5"   
   �� 5#      $��* �6   �� 5$      $��2* �6    � RowDefinitions 5&   
   �� 5'      $��* �6   �� 5(      $��* �6   �� 5)      $��* �6   �� 5*      $��* �6   �� 5+      $��* �6   �� 5,      $��* �6   .+�5/   
   �� $��0��6   $��0��50      $	��Top=�51      ��52      ��   Locator �� Source# [  �� Path$- 'LocalizationHelper.[Resources.Language]� �� StringFormat$ {0} :���� 55   
   $��0��6   $��1��56      $
��Left=�57      $	��Top=�58       �� SelectedValuePath$	 Key��5A       � DataContext 59      �� (   "Messenger.Locator.ViewModelLocator   Instance# Z $ .� �� UpdateSourceTrigger$ PropertyChanged=���5<      �� $ 	Languages�$ PropertyChanged=� �� SelectedValue 5>      �� $ Language�$ PropertyChanged=� �� Mode$ TwoWay=���5B      �� 5C      .R�5D      � .+�5E      �� $��16��6    $��11��5F   $   ��5G   $   �� $	 Key� �� 	Converter# [ �� 5I      $��5,0,0,0q�6$   
���   ���5K   (   �� $ Value�5L      5M      5O      �� 5Q   
   $��1��6   $��0��5R      ��5S      �� # [ $- 'LocalizationHelper.[Resources.UserName]�$ {0} :���� 5V   
   $��1��6   $��1��5W      $	��120��5X      $
��Left=�5Y      ��5Z      �� #  Messenger.Properties.Settings   Default# Z $ UserName��� 5p   
   $��2��6   $��0��5q      ��5r      �� # [ $- 'LocalizationHelper.[Resources.Encoding]�$ {0} :���� 5v   
   -   5        $��comboBoxEncodings��5v      $��2��5w      $��1��5x      $	��120��5y      $
��Left=�5z      $��Center=�5{       �� 
IsEditable .  �� Text 5}      �� # Z $ TextEncoding��� 5   
   $��3��6   $��0��5�      ��5�      �� # [ $/ )LocalizationHelper.[Resources.PortNumber]�$ {0} :���� 5�   
   $��3��6   $��1��5�      $	��120��5�      $
��Left=�5�      ��5�      �� # Z $ 
PortNumber��� 5�   
   $��4��6   $��1��5�      $
��Left=�5�      $��Center=�5�      ��5�      �� # [ $: 4LocalizationHelper.[Resources.ActivateComingMessage]� d� 	IsChecked 5�      �� # Z $ ActivateComingMessage��� 5�   
   -   5        $��5��5�      $��2��5�      $��80��5�      $��26��5�      $��Center=�5�      $��Center=�5�      $��OK��5�      5�      5�      