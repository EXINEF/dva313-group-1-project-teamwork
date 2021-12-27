from django.forms.models import ModelForm
from .models import Vehicle, Tire, Sensor
from django.contrib.auth.forms import UserCreationForm, UserChangeForm
from django.contrib.auth.models import User

class VehicleForm(ModelForm):
    class Meta:
        model = Vehicle
        fields = '__all__'
        exclude = ('locations','company',)

    def __init__(self, *args, company, **kwargs):
        super().__init__(*args, **kwargs)
        # TODO implement to exclude the id of the tires that are in the current use
        # ids = ['777FLT','777FRT']
        self.fields['tire_left_front'].queryset = Tire.objects.filter(company=company, is_used=False)#.exclude(id__in=ids)
        self.fields['tire_left_rear'].queryset = Tire.objects.filter(company=company, is_used=False)
        self.fields['tire_right_front'].queryset = Tire.objects.filter(company=company, is_used=False)
        self.fields['tire_right_rear'].queryset = Tire.objects.filter(company=company, is_used=False)
    
class VehicleFormOnlyTires(VehicleForm):
    class Meta:
        model = Vehicle
        #
        fields = ('tire_left_front','tire_left_rear','tire_right_front','tire_right_rear')  

class TireForm(ModelForm):
    class Meta:
        model = Tire
        fields = '__all__'
        exclude = ('company',)

class SensorForm(ModelForm):
    class Meta:
        model = Sensor
        fields = '__all__'
        exclude = ('company',)

class CreateUserForm(UserCreationForm):
	class Meta:
		model = User
		fields = ['username', 'first_name', 'last_name', 'email', 'password1', 'password2']
    
class EditFleetManagerForm(UserChangeForm):
	class Meta:
		model = User
		fields = ['username','first_name', 'last_name', 'email']