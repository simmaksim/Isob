   M S B A M L   `   `   `  ���� F  BMDiSUBD_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MDiSUBD_WPFMDiSUBD_WPF      MDiSUBD_WPF.AddWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:MDiSUBD_WPF     -     � Title$  Adding Order��$	��450��$	��800�� � Icon$< 6C:\PTUIR\5 semestr\MDiSUBD\MDiSUBD_WPF\Images\Logo.jpg��.��� .+��� $��Заказчик��$
��Left=�$��	33,10,0,0q�$	��Top=��� $��СТО��$
��Left=�$��	33,53,0,0q�$	��Top=��� $��Номер заказа��$
��Left=�$��	33,84,0,0q�$	��Top=��� $��Запчасть��$
��Left=�$��
33,127,0,0q�$	��Top=��� $��Фото��$
��Left=�$��
33,178,0,0q�$	��Top=��� $��Мастер��$
��Left=�$��
33,225,0,0q�$	��Top=��� $��Работа��$
��Left=�$��
33,265,0,0q�$	��Top=��� $��Оплата��$
��Left=�$��
33,313,0,0q�$	��Top=��� -   $��customerComboBox��$
��Left=�$��
184,10,0,0q�$	��Top=�$	��120�� �� SelectedIndex$ 0������ .R���  �� Orientation$ 
Horizontal=�.+��� �� ��� *customer_id+ �� $�  ���� �� ��� *customer_name+�� -   $��addOrderButton��$!��Добавить заказ��$
��Left=�$��430,173,0,0q�$	��Top=�$	��139��$��23���� -   $��stationComboBox��$
��Left=�$��
184,53,0,0q�$	��Top=�$	��120��$ 0������ .R��� $ 
Horizontal=�.+��� �� ��� *workshop_id+ �� $� �� �� $�  ���� �� ��� *workshop_name+ �� $� ���� �� ��� *workshop_address+�� -   $��detailComboBox��$
��Left=�$��184,127,0,0q�$	��Top=�$	��120��$ 0������ .R��� $ 
Horizontal=�.+��� �� ��� *	detail_id+ �� $� �� �� ��� *detail_name+ �� $� �� �� ��� *detail_producer+ �� $� �� �� ��� *detail_price+�� -   $��masterComboBox��$
��Left=�$��184,220,0,0q�$	��Top=�$	��120��$ 0������ .R��� $ 
Horizontal=�.+��� �� ��� *	master_id+ �� $� �� �� ��� *master_name+ �� $� �� �� ��� *master_category+�� -   $��workComboBox��$
��Left=�$��184,265,0,0q�$	��Top=�$	��120��$ 0������ .R��� $ 
Horizontal=�.+��� �� ��� *	work_id+ �� $� �� �� ��� *	work_type+ �� $� �� �� ��� *
work_price+�� -   $��paymentComboBox��$
��Left=�$��184,308,0,0q�$	��Top=�$	��120��$ 0������ .R��� $ 
Horizontal=�.+��� �� ��� *
payment_id+ �� $� �� �� ��� *payment_name+�� -	    ��Name$ photoComboBox��$
��Left=�$��184,173,0,0q�$	��Top=�$	��120��$ 0������ .R��� $ 
Horizontal=�.+��� �� ��� *car_id+ �� $� �� �� ��� *
car_path+�� -
   $��orderNumberTextBox��$
��Left=�$��23��$��
184,87,0,0q� �� TextWrapping$
 Wrap=�$��0��$	��Top=�$	��120��