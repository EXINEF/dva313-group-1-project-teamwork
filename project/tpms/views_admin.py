from django.shortcuts import get_object_or_404, redirect, render
from django.contrib.auth.forms import AuthenticationForm
from django.contrib.auth import authenticate, login, logout
from .models import FleetManager, Vehicle, Tire, Sensor
from .forms import VehicleForm, CreateUserForm
from .decorators import unauthenticated_user, company_administrator_only, allowed_users
from django.contrib.auth.decorators import login_required
from django.contrib import messages

@login_required(login_url='index')
@company_administrator_only
def adminPage(request):
    fleet_managers = FleetManager.objects.all()

    context = {'fleet_managers':fleet_managers}
    return render(request, 'admin/admin-page.html', context)
    
@login_required(login_url='index')
@company_administrator_only
def addFleetManager(request):   
    form = CreateUserForm()
    if request.method == 'POST':
        form = CreateUserForm(request.POST)
        if form.is_valid():
            user = form.save()
            username = form.cleaned_data.get('username')
            messages.success(request, 'Fleet Manager Account was created for ' + username)
            return redirect('admin-page')
    context = {'form':form}
    return render(request, 'admin/add-fleet-manager.html', context)


