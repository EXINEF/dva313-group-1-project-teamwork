from django.shortcuts import get_object_or_404, redirect, render
from django.contrib.auth.forms import AuthenticationForm
from django.contrib.auth import authenticate, login, logout
from .models import FleetManager, Vehicle, Tire, Sensor
from .forms import VehicleForm, CreateUserForm
from .decorators import unauthenticated_user, company_administrator_only, allowed_users
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
            messages.error(request, 'Username OR password is incorrect')

    context = {}
    return render(request,'index/index.html', context)

@login_required(login_url='index')
def logoutPage(request):
    logout(request)
    return redirect('index')

@login_required(login_url='index')
@allowed_users(allowed_roles=['fleet-manager'])
def homePage(request):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicles = Vehicle.objects.filter(company=fleet_manager.company)

    context = {'vehicles':vehicles}
    return render(request, 'user/home.html', context)

def vehiclePage(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicle = get_object_or_404(Vehicle, id=pk, company=fleet_manager.company)
    tires = vehicle.tires.all()
    location = vehicle.locations.all().order_by('-creation_datetime').first()

    context = {'vehicle':vehicle, 'tires':tires, 'location':location}
    return render(request, 'user/vehicle/vehicle.html', context)

def addVehiclePage(request):
    form = VehicleForm()
    if request.method == 'POST':
        form = VehicleForm(request.POST)
        if form.is_valid():
            form.save()
            messages.success(request,'The new vehicle was addedd successfuly')
            return redirect('home')

    context = {'form':form}
    return render(request, 'user/vehicle/add-vehicle.html', context)

def updateVehiclePage(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicle = get_object_or_404(Vehicle, id=pk, company=fleet_manager.company)
    form = VehicleForm(instance=vehicle)
    
    if request.method == 'POST':
        form = VehicleForm(request.POST, instance=vehicle)

        if form.is_valid():
            form.save()
            messages.success(request,'The vehicle %s is been update successfuly' % vehicle)
            return redirect('home')
        else:
            messages.error(request,'Error while updating task: '+str(form.errors))

    
    context = {'vehicle':vehicle, 'form':form}
    return render(request, 'user/vehicle/update-vehicle.html', context)

def deleteVehiclePage(request, pk):
    fleet_manager = FleetManager.objects.get(user=request.user)
    vehicle = get_object_or_404(Vehicle, id=pk, company=fleet_manager.company)
    
    if request.method == 'POST':
        messages.success(request,'The vehicle %s was deleted successfuly' % vehicle)
        Vehicle.delete(vehicle)
        return redirect('/')

    context = {'vehicle':vehicle}
    return render(request, 'user/vehicle/delete-vehicle.html', context)

def tirePage(request, pk):
    tire = get_object_or_404(Tire, id=pk)

    context = {'tire':tire}
    return render(request, 'user/tire/tire.html', context)

def addTirePage(request):

    context = {}
    return render(request, 'user/tire/add-tire.html', context)
    
def sensorPage(request, pk):
    sensor = get_object_or_404(Sensor, id=pk)

    context = {'sensor':sensor}
    return render(request, 'user/sensor/sensor.html', context)

def addSensorPage(request):
    
    context = {}
    return render(request, 'user/sensor/add-sensor.html', context)
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
