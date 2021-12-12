from django.forms.models import ModelForm
from .models import Vehicle, Tire, Sensor
from django.contrib.auth.forms import UserCreationForm, UserChangeForm
from django.contrib.auth.models import User

class VehicleForm(ModelForm):
    class Meta:
        model = Vehicle
        fields = '__all__'
        # exclude = ('locations','company')

class TireForm(ModelForm):
    class Meta:
        model = Tire
        fields = '__all__'

class SensorForm(ModelForm):
    class Meta:
        model = Sensor
        fields = '__all__'

class CreateUserForm(UserCreationForm):
	class Meta:
		model = User
		fields = ['username', 'first_name', 'last_name', 'email', 'password1', 'password2']
    
class EditFleetManagerForm(UserChangeForm):
	class Meta:
		model = User
		fields = ['username','first_name', 'last_name', 'email']