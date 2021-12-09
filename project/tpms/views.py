from django.shortcuts import get_object_or_404, redirect, render
from django.contrib.auth.forms import AuthenticationForm
from django.contrib.auth import authenticate, login, logout
from .models import FleetManager, Vehicle, Tire, Sensor
from .forms import VehicleForm, CreateUserForm
from .decorators import unauthenticated_user, admin_only, allowed_users
from django.contrib.auth.decorators import login_required
from django.contrib import messages


@unauthenticated_user
def indexPage(request):
    if request.method == 'POST':
        username = request.POST.get('username')
        password = request.POST.get('password')

        user = authenticate(request, username=username, password=password)

        if user is not None:
            login(request, user)
            return redirect('admin-page')
        else:
            messages.info(request, 'Username OR password is incorrect')

    context = {}
    return render(request,'index/index.html', context)

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

@login_required(login_url='index')
#@admin_only
def adminPage(request):
    fleet_managers = FleetManager.objects.all()

    context = {'fleet_managers':fleet_managers}
    return render(request, 'admin/admin-page.html', context)

def logoutPage(request):
    logout(request)
    return redirect('index')

@login_required(login_url='index')
@allowed_users(allowed_roles=['fleet-manager'])
def homePage(request):
    vehicles = Vehicle.objects.all()

    context = {'vehicles':vehicles}
    return render(request, 'user/home.html', context)

def vehiclePage(request, pk):
    vehicle = get_object_or_404(Vehicle, id=pk)
    tires = vehicle.tires.all()
    location = vehicle.locations.all().order_by('-creation_datetime').first()

    context = {'vehicle':vehicle, 'tires':tires, 'location':location}
    return render(request, 'user/vehicle.html', context)

def addVehiclePage(request):
    form = VehicleForm()
    if request.method == 'POST':
        form = VehicleForm(request.POST)
        if form.is_valid():
            form.save()
            return redirect('home')

    context = {'form':form}
    return render(request, 'user/add-vehicle.html', context)

def tirePage(request, pk):
    tire = get_object_or_404(Tire, id=pk)

    context = {'tire':tire}
    return render(request, 'user/tire.html', context)

def sensorPage(request, pk):
    sensor = get_object_or_404(Sensor, id=pk)

    context = {'sensor':sensor}
    return render(request, 'user/sensor.html', context)

# http://127.0.0.1:8000/savedata/AUTHSYSTEM-TEST111-SENSORTEST1-3234-223-BROKEN-SENSORTEST2-40234-222-WORKING-SENSORTEST3-2237-32345-DANGER-SENSORTEST4-5453-1354-WARNING
def saveData(request, query):
    chunks = query.split('-')
    if chunks[0] == 'AUTHSYSTEM':
        vehicle = get_object_or_404(Vehicle,id=chunks[1])
        tires = vehicle.tires.all()
        counter = 0
        for x in range(2, len(chunks), 4):
            sensor = tires[counter].sensor
            sensor.temperature = chunks[x+1]
            sensor.pressure = chunks[x+2]
            sensor.status = chunks[x+3]
            sensor.save()
            counter+=1
            
    return redirect('/')
