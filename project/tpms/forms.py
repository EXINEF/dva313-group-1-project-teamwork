from django.forms.models import ModelForm
from .models import Vehicle
from django.contrib.auth.forms import UserCreationForm
from django.contrib.auth.models import User

class VehicleForm(ModelForm):
    class Meta:
        model = Vehicle
        fields = '__all__'
        # exclude = ('locations','company')

class CreateUserForm(UserCreationForm):
	class Meta:
		model = User
		fields = ['username', 'first_name', 'last_name', 'email', 'password1', 'password2']