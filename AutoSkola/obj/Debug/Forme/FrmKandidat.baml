   M S B A M L   `   `   `  ����D  @AutoSkola, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:AutoSkola.FormeAutoSkola.Forme  5   	   !    AutoSkola.Forme.FrmKandidat   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:AutoSkola.Forme     5   	     � Title$  Dodaj / Izmeni Kandidata��5   	   $	��440��6*   $	��350��67   .��5	      � .+�5
   
   �� $
��Ime:��6   $
��Left=�6   $��	30,30,0,0q�6:   $	��Top=�6M   �� 5   
   $��Prezime:��6   $
��Left=�6#   $��	30,60,0,0q�6>   $	��Top=�6Q   �� 5   
   $��Kontakt:��6   $
��Left=�6#   $��	30,90,0,0q�6>   $	��Top=�6Q   �� 5   
   $��JMBG:��6   $
��Left=�6    $��
30,120,0,0q�6;   $	��Top=�6O   �� 5   
   $��Adresa Stanovanja:��6   $
��Left=�6-   $��
30,150,0,0q�6H   $	��Top=�6\   �� 5   
   $��Datum Rodjenja:��6   $
��Left=�6*   $��
30,180,0,0q�6E   $	��Top=�6Y   �� 5   
   $��Lekarski Pregled:��6   $
��Left=�6,   $��
30,210,0,0q�6G   $	��Top=�6[   �� 5   
   $��Prva Pomoc:��6   $
��Left=�6&   $��
30,240,0,0q�6A   $	��Top=�6U   �� 5   
   $��Teorijski Ispit:��6   $
��Left=�6+   $��
30,270,0,0q�6F   $	��Top=�6Z   �� 5   
   $��Prakticni Ispit:��6   $
��Left=�6+   $��
30,300,0,0q�6F   $	��Top=�6Z   �� 5   
   -   5         ��Name$ txtIme��5      $
��Left=�6"   $��
180,35,0,0q�6=    �� TextWrapping$
 Wrap=�6Q   $�� ��6e   $	��Top=�6m   $	��130��6�   �� 5   
   -   5        $ 
txtPrezime��5      $
��Left=�6&   $��
180,65,0,0q�6A   $
 Wrap=�6U   $�� ��6i   $	��Top=�6q   $	��130��6�   �� 5   
   -   5        $ 
txtKontakt��5      $
��Left=�6&   $��
180,95,0,0q�6A   $
 Wrap=�6U   $�� ��6i   $	��Top=�6q   $	��130��6�   �� 5   
   -   5        $ txtJMBG��5      $
��Left=�6#   $��180,125,0,0q�6>   $
 Wrap=�6S   $�� ��6g   $	��Top=�6o   $	��130��6�   �� 5   
   -   5        $ 	txtAdresa��5      $
��Left=�6%   $��180,155,0,0q�6@   $
 Wrap=�6U   $�� ��6i   $	��Top=�6q   $	��130��6�   �� 5   
   -   5        $ txtDatumRodjenja��5      $
��Left=�6,   $��180,185,0,0q�6G   $
 Wrap=�6\   $�� ��6p   $	��Top=�6x   $	��130��6�   �� 5   
   -   5         ��Name$ 
btnSacuvaj��5      $��Sacuvaj��6%   $
��Left=�67   $��100,360,0,0q�6R   $	��Top=�6g   
�������6�   �� 5    
   -   5        $ 	btnOtkazi��5       $��Otkazi��6$   $
��Left=�65   $��200,360,0,0q�6P   $	��Top=�6e   
�������6�   �� 5!   
   -	   5         ��Name$ 
cbLekarski��5!      $
��Left=�6'   $��180,214,0,0q�6B   $	��Top=�6W   $	��130��6o    �� DisplayMemberPath$ 
lekarskiID��6{    �� SelectedValuePath$ 
lekarskiID��6�   �� 5"   
   -
   5        $
 cbPP��5"      $
��Left=�6!   $��180,244,0,0q�6<   $	��Top=�6Q   $	��130��6i   $ prvaPomocID��6u   $ prvaPomocID��6�   �� 5#   
   -   5        $ cbTeorijski��5#      $
��Left=�6(   $��180,274,0,0q�6C   $	��Top=�6X   $	��130��6p   $ teorijskiID��6|   $ teorijskiID��6�   �� 5$   
   -   5        $ cbPrakticni��5$      $
��Left=�6(   $��180,304,0,0q�6C   $	��Top=�6X   $	��130��6p   $ prakticniID��6|   $ prakticniID��6�   5&      5'      